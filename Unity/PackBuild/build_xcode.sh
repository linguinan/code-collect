#!/bin/sh
 
#参数判断  
# if [ $# != 1 ];then  
#     echo "需要一个参数。 参数是游戏包的名子"  
#     exit     
# fi  
 
#UNITY程序的路径#
UNITY_PATH=/Applications/Unity/Unity.app/Contents/MacOS/Unity

PACK_PATH=$(cd `dirname $0`; pwd)
 
#游戏程序路径#
PROJECT_PATH=${PACK_PATH%/*}
echo $PROJECT_PATH
 
#IOS打包脚本路径#
BUILD_IOS_PATH=$PACK_PATH/build_ios_dev.sh

#$1
XCODE_NAME=XcodeProject
 
#生成的Xcode工程路径#
XCODE_PATH=$PACK_PATH/$XCODE_NAME
 
#将unity导出成xcode工程#
# $UNITY_PATH -batchmode -projectPath $PROJECT_PATH -executeMethod BuildPack.BuildReleaseIOS -logFile $PACK_PATH/log.txt -quit project-$XCODE_NAME
 
if [ $# != 1 ];then  
    $UNITY_PATH -batchmode -projectPath $PROJECT_PATH -executeMethod BuildPack.BuildReleaseIOS -logFile $PACK_PATH/log.txt -quit project-$XCODE_NAME
else
    $UNITY_PATH -batchmode -projectPath $PROJECT_PATH -executeMethod BuildPack.BuildReleaseIOS -logFile $PACK_PATH/log.txt -quit project-$XCODE_NAME ver-$1 incbuild
fi  

echo "XCODE工程生成完毕"
 
#开始生成ipa#
$BUILD_IOS_PATH $XCODE_PATH $XCODE_NAME
 
echo "ipa生成完毕"


#如果报错：xcode-select: error: tool 'xcodebuild' requires Xcode
#可能是之前装过多个xcode导致的，执行以下命令即可
#sudo xcode-select --switch /Applications/Xcode.app/Contents/Developer/