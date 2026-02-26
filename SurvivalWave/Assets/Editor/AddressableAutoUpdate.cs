#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class AddressablesDeployToGhPages
{
    private static readonly string SourceBuildDir = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "ServerData/StandaloneWindows64"));
    private static readonly string GhPagesRepoDir = @"C:\Users\JHD\Documents\GitHub\TestPage";
    private static readonly string GhPagesTargetSubDir = Path.Combine("StandaloneWindows64");

    private const string CommitMessage = "Update Addressables content";

    [MenuItem("Tools/Addressables/Build + Deploy (GitHub Pages)")]
    public static void BuildAndDeploy()
    {
        AddressableAssetSettings.BuildPlayerContent();
        Deploy();
    }

    private static void Deploy()
    {
        var src = SourceBuildDir;
        var dst = Path.Combine(GhPagesRepoDir, GhPagesTargetSubDir);

        if (!Directory.Exists(src))
        {
            UnityEngine.Debug.LogError($"[Deploy] Source build dir not found: {src}");
            return;
        }
        if (!Directory.Exists(GhPagesRepoDir))
        {
            UnityEngine.Debug.LogError($"[Deploy] Git repo dir not found: {GhPagesRepoDir}");
            return;
        }

        if (Directory.Exists(dst)) Directory.Delete(dst, true);
        Directory.CreateDirectory(dst);

        CopyDirectory(src, dst);

        RunGit("add -A", GhPagesRepoDir);

        var changes = RunGit("status --porcelain", GhPagesRepoDir, captureOutput: true);
        if (string.IsNullOrWhiteSpace(changes))
        {
            UnityEngine.Debug.Log("[Deploy] No changes to commit.");
            return;
        }

        RunGit($"commit -m \"{CommitMessage}\"", GhPagesRepoDir);
        RunGit("push", GhPagesRepoDir);

        UnityEngine.Debug.Log($"[Deploy] Done. Copied from:\n{src}\nTo:\n{dst}");
    }

    private static void CopyDirectory(string sourceDir, string destDir)
    {
        foreach (var dir in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            Directory.CreateDirectory(dir.Replace(sourceDir, destDir));

        foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
        {
            var target = file.Replace(sourceDir, destDir);
            Directory.CreateDirectory(Path.GetDirectoryName(target)!);
            File.Copy(file, target, true);
        }
    }

    private static string RunGit(string args, string workDir, bool captureOutput = false)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = args,
            WorkingDirectory = workDir,
            UseShellExecute = false,
            RedirectStandardOutput = captureOutput,
            RedirectStandardError = captureOutput,
            CreateNoWindow = true
        };

        using var p = Process.Start(psi);
        if (p == null) throw new Exception("Failed to start git process.");

        string output = "";
        if (captureOutput)
            output = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();

        p.WaitForExit();

        if (p.ExitCode != 0)
            UnityEngine.Debug.LogError($"[Deploy] git {args} failed (code {p.ExitCode})\n{output}");

        return output;
    }
}
#endif