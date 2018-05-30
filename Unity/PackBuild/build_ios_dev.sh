#!/bin/bash  
 
#参数判断  
if [ $# != 2 ];then  
    echo Params error!  
    echo Need two params: 1.path of project 2.name of ipa file  
    exit  
elif [ ! -d $1 ];then  
    echo The first param is not a dictionary.  
    exit      
fi  

#当前文件路径
PACK_PATH=$(cd `dirname $0`; pwd)
 
#工程路径  
project_path=$1  
 
#IPA名称  
# ipa_name=$2  
 
#build文件夹路径  
build_path=${project_path}/build  
 
#清理#
xcodebuild clean
 
#编译工程  
cd $project_path  
xcodebuild || exit  
 
#打包
# xcrun -sdk iphoneos PackageApplication -v ${build_path}/*.app -o ${build_path}/${ipa_name}.ipa
#PackageApplication在8.3开始废弃了
#PackageApplication is deprecated, use `xcodebuild -exportArchive` instead.

#编译打包成Archive
# workspaceName=Unity-iPhone.xcworkspace
projectName=Unity-iPhone.xcodeproj
#项目名称
scheme=Unity-iPhone
configurationBuildDir=$project_path/build
configuration=Release
archivePath=$project_path/build/Unity-iPhone.xcarchive
# 用默认的，至少用手动用xcode打包一次，确保以下配置文件都正确
# codeSignIdentity=iPhone Distribution: XXX, Ltd. (xxxxxxxxxx)
# adHocProvisioningProfile=xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
# appStoreProvisioningProfile=xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx

log_path=$PACK_PATH/log.txt
#AppStoreExportOptions.plist
exportOptionsPlist=$PACK_PATH/DevelopmentExportOptions.plist
now=`date "+%Y-%m-%d %H-%M-%S"`

# 如果是workspace就用-workspace，就像编译带有CocoaPods的项目，如果是普通项目则用-project
xcodebuild archive -project $projectName -scheme $scheme -configuration $configuration -archivePath $archivePath >> $log_path
# CONFIGURATION_BUILD_DIR=$configurationBuildDir $log_pathCODE_SIGN_IDENTITY=$codeSignIdentity PROVISIONING_PROFILE=$provisioningProfile

echo archive done!

#将Archive导出
xcodebuild -exportArchive -archivePath $archivePath -exportOptionsPlist $exportOptionsPlist -exportPath $PACK_PATH/$scheme"-Dev" $now >> $log_path

echo exportArchive done!