@echo start

set CUR_PATH=%cd%
cd..

set UNITY_PATH="D:\Program Files\Unity\Editor\Unity.exe"
set PROJECT_PATH=%cd%


%UNITY_PATH% -batchmode -projectPath %PROJECT_PATH% -executeMethod BuildPack.BuildReleaseAndroid -logFile %CUR_PATH%\log.txt -quit


@echo end
pause