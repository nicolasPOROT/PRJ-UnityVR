using UnityEditor.Build;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine.Rendering;
if (EditorApplication.isPlaying || EditorApplication.isCompiling) throw new InvalidOperationException("Hors Play, compilation terminée requise.");
PlayerSettings.companyName = "ST2VAAS";
PlayerSettings.productName = "Fruit Ninja VR";
PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "fr.st2vaas.fruitninjavr.ben");
PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
PlayerSettings.colorSpace = ColorSpace.Linear;
PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLES3 });
EditorBuildSettings.scenes = new[] {
    new EditorBuildSettingsScene("Assets/Scenes/MainMenu.unity", true),
    new EditorBuildSettingsScene("Assets/Scenes/PlayScene.unity", true)
};
var settings = AssetDatabase.LoadAssetAtPath<XRGeneralSettingsPerBuildTarget>("Assets/XR/XRGeneralSettingsPerBuildTarget.asset");
if (!settings.HasSettingsForBuildTarget(BuildTargetGroup.Android)) settings.CreateDefaultSettingsForBuildTarget(BuildTargetGroup.Android);
if (!settings.HasManagerSettingsForBuildTarget(BuildTargetGroup.Android)) settings.CreateDefaultManagerSettingsForBuildTarget(BuildTargetGroup.Android);
var general = settings.SettingsForBuildTarget(BuildTargetGroup.Android);
general.InitManagerOnStart = true;
if (!XRPackageMetadataStore.AssignLoader(general.Manager, "UnityEngine.XR.OpenXR.OpenXRLoader", BuildTargetGroup.Android)) throw new InvalidOperationException("OpenXR Android non assigné.");
var xr = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.Android);
foreach (var feature in xr.GetFeatures<OpenXRFeature>())
{
    if (new[] { "OculusTouchControllerProfile", "MetaQuestTouchPlusControllerProfile", "MetaQuestTouchProControllerProfile", "MetaQuestFeature" }.Contains(feature.GetType().Name)) feature.enabled = true;
    EditorUtility.SetDirty(feature);
}
EditorUtility.SetDirty(xr); EditorUtility.SetDirty(general); EditorUtility.SetDirty(general.Manager); EditorUtility.SetDirty(settings);
AssetDatabase.SaveAssets();
var validations = new List<object>();
foreach (var target in new[] { BuildTargetGroup.Standalone, BuildTargetGroup.Android })
{
    var issues = new List<OpenXRFeature.ValidationRule>();
    UnityEditor.XR.OpenXR.OpenXRProjectValidation.GetCurrentValidationIssues(issues, target);
    validations.Add(new { target = target.ToString(), issues = issues.Select(i => new { i.message, i.error, i.fixItAutomatic }).ToArray() });
}
return new { company = PlayerSettings.companyName, product = PlayerSettings.productName, androidId = PlayerSettings.GetApplicationIdentifier(NamedBuildTarget.Android), validation = validations };
