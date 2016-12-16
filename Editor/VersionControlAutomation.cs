using UnityEditor;
using UnityEngine;

namespace Binocle
{
    [InitializeOnLoad]
    public class VersionControlAutomation
    {
        static VersionControlAutomation()
        {
            //If you want the scene to be fully loaded before your startup operation,
            // for example to be able to use Object.FindObjectsOfType, you can defer your
            // logic until the first editor update, like this:
            EditorApplication.update += RunOnce;
        }

        static void RunOnce()
        {
            EditorApplication.update -= RunOnce;
            ReadVersionAndIncrement();
        }

        static void ReadVersionAndIncrement()
        {
            //the file name and path.  No path is the base of the Unity project directory (same level as Assets folder)
            string versionTextFileNameAndPath = "version.txt";

            string versionText = Utils.ReadTextFile(versionTextFileNameAndPath);

            if (versionText != null)
            {
                versionText = versionText.Trim(); //clean up whitespace if necessary
                string[] lines = versionText.Split('.');

                int MajorVersion = int.Parse(lines[0]);
                int MinorVersion = int.Parse(lines[1]);
                int SubMinorVersion = int.Parse(lines[2]) + 1; //increment here
                string SubVersionText = lines[3].Trim();

                Debug.Log("Major, Minor, SubMinor, SubVerLetter: " + MajorVersion + " " + MinorVersion + " " + SubMinorVersion + " " + SubVersionText);

                versionText = MajorVersion.ToString("0") + "." +
                              MinorVersion.ToString("0") + "." +
                              SubMinorVersion.ToString("000") + "." +
                              SubVersionText;

                Debug.Log("Version Incremented " + versionText);

                //save the file (overwrite the original) with the new version number
                Utils.WriteTextFile(versionTextFileNameAndPath, versionText);
                //save the file to the Resources directory so it can be used by Game code
                Utils.WriteTextFile("Assets/Resources/version.txt", versionText);

                //tell unity the file changed (important if the versionTextFileNameAndPath is in the Assets folder)
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log("Creating new default version file");
                //no file at that path, make it
                Utils.WriteTextFile(versionTextFileNameAndPath, "0.0.0.dev");
            }
        }
    }
}