using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

class MyEditorScript
{
    public static void PerformBuild()
    {
        try
        {
            BuildAddressablesOrThrow();
            BuildPlayerOrThrow();
        }
        catch (Exception e)
        {
            Debug.LogError("[JenkinsBuild] BUILD FAILED\n" + e);
            EditorApplication.Exit(1);
        }

        EditorApplication.Exit(0);
    }

    static void BuildAddressablesOrThrow()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
            throw new Exception("AddressableAssetSettingsDefaultObject.Settings is null");

        AddressableAssetSettings.CleanPlayerContent(settings.ActivePlayerDataBuilder);

        AddressablesPlayerBuildResult result;
        AddressableAssetSettings.BuildPlayerContent(out result);

        if (!string.IsNullOrEmpty(result.Error))
            throw new Exception("Addressables build failed: " + result.Error);

        Debug.Log("[JenkinsBuild] Addressables build success.");
    }
    static void BuildPlayerOrThrow()
    {
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        if (scenes.Length == 0)
            throw new Exception("No enabled scenes in Build Settings.");

        var outputPath = "Build/SurvivalWave.exe"; 
        var options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result != BuildResult.Succeeded)
            throw new Exception($"Player build failed: {report.summary.result}\n{report.summary.outputPath}");

        Debug.Log("[JenkinsBuild] Player build success: " + report.summary.outputPath);
    }
}