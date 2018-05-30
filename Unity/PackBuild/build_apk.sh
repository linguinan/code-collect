#!/bin/sh

PACK_PATH=$(cd `dirname $0`; pwd)

#Unity路径#
UNITY_PATH=/Applications/Unity/Unity.app/Contents/MacOS/Unity

#游戏程序路径#
PROJECT_PATH=${PACK_PATH%/*}


if [ $# != 1 ];then  
    $UNITY_PATH -batchmode -projectPath $PROJECT_PATH -executeMethod BuildPack.BuildReleaseAndroid -logFile $PACK_PATH/log.txt -quit
else
    $UNITY_PATH -batchmode -projectPath $PROJECT_PATH -executeMethod BuildPack.BuildReleaseAndroid -logFile $PACK_PATH/log.txt -quit ver-$1 incbuild
fi  

echo "apk打包完毕"