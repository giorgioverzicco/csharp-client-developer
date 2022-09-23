var path = @"..\..\..\..\developer_activity.txt";

while (true)
{
    string[] activities;
    
    try
    {
        activities = File.ReadAllLines(path);
    }
    catch (Exception)
    {
        activities = Array.Empty<string>();
    }

    if (activities.Length == 0)
    {
        Console.Clear();
        Console.WriteLine("No activities, found. Waiting...");
        Thread.Sleep(1000);
    }
    
    foreach (var activity in activities)
    {
        Console.WriteLine($"Task taken in charge: {activity}");

        var retry = false;

        do
        {
            try
            {
                List<string> updatedActivities = new (File.ReadAllLines(path));
                var isDone = new Random().NextDouble() > 0.5;

                if (isDone)
                {
                    updatedActivities.Remove(activity);
                    File.WriteAllLines(path, activities.ToArray());
                    
                    Console.WriteLine($"Task done: {activity}");
                }
                else
                {
                    updatedActivities.Remove(activity);
                    updatedActivities.Add(activity);
                    File.WriteAllLines(path, activities.ToArray());
                    
                    Console.WriteLine($"Task still not done, will do later: {activity}");
                }

                retry = false;
            }
            catch (Exception)
            {
                retry = true;
                Console.WriteLine("\n[ERROR] Impossible to complete the task, retrying...\n");
            }
        } while (retry);
    }
}