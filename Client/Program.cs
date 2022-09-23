var clientPath = @"..\..\..\..\client_activity.txt";
var developerPath = @"..\..\..\..\developer_activity.txt";

string[] clientActivities = File.ReadAllLines(clientPath);
string[] developerActivities = File.ReadAllLines(developerPath);

foreach (var activity in clientActivities)
{
    Console.WriteLine($"Watching for: {activity}");

    if (developerActivities.Contains(activity))
    {
        Console.WriteLine($"This task was already assigned: {activity}\n");
        continue;
    }

    var retry = false;

    do
    {
        StreamWriter? developerFile = null;
        try
        {
            Console.WriteLine($"Assigning task: {activity}");
            developerFile = File.AppendText(developerPath);
            developerFile.WriteLine(activity);
            retry = false;
        }
        catch (Exception)
        {
            Console.WriteLine("\n[ERROR] Can't assign the task right now, retrying...");
            retry = true;
        }
        finally
        {
            developerFile?.Close();
        }
    } while (retry);
}