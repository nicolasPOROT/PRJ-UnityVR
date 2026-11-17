# Releve automatise du contenu ZIP de l'APK

Analyse de la structure ZIP uniquement ; Android Studio APK Analyzer et execution sur casque NON REALISES.

SHA256 : `BEB89D96902D651B30F116E4FB14DF865CA2C7EEDD5E901E8D40C60E258053FC`

Mo = octets / 1 048 576. Les sommes ne comprennent pas les en-tetes et signatures ZIP.

| Entree / groupe | Fichiers | Mo compresses | Mo decomprimees |
|---|---:|---:|---:|
| assets/bin/Data | 155 | 26.57 | 55.04 |
| lib/arm64-v8a/libil2cpp.so | 1 | 15.37 | 61.65 |
| lib/arm64-v8a/libunity.so | 1 | 9.07 | 20.61 |
| classes*.dex | 1 | 2.25 | 6.16 |
| resources.arsc | 1 | 0.24 | 0.24 |

## Dix entrees les plus volumineuses (taille decompressee)

| Entree | Octets compresses | Octets decomprimes |
|---|---:|---:|
| lib/arm64-v8a/libil2cpp.so | 16115310 | 64641456 |
| lib/arm64-v8a/libunity.so | 9512117 | 21612800 |
| assets/bin/Data/Managed/Metadata/global-metadata.dat | 2616809 | 9869788 |
| classes.dex | 2355269 | 6457364 |
| assets/bin/Data/Resources/unity default resources | 798282 | 3743124 |
| lib/arm64-v8a/libopenxr_loader.so | 462005 | 1583992 |
| lib/arm64-v8a/libUnityOpenXR.so | 479277 | 1403144 |
| lib/arm64-v8a/libc++_shared.so | 406563 | 1292904 |
| assets/bin/Data/c9adb1f542a93ae489bacfeae21e8394 | 13525 | 1053576 |
| assets/bin/Data/sharedassets0.assets.split11 | 530314 | 1048576 |
