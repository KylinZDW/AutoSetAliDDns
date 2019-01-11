# AutoSetAliDDns
自动获取本机外网IP并注册到阿里云的域名解析上

# 开发环境
visual studio 2015，兼容高版本和部分低版本

# 使用方式
1. 使用visual studio打开工程（\DDNSClient\DDNSClient.sln）
2. 修改 \DDNSClient\DDNSClient.Service\App.config ,
并填写：
   a. 阿里云开发者的AccessKey
   b. 阿里云开发者的 AccessKeySecret
   c. 要进行解析的域名
3. 重新生成工程或重新生成工程解决方案
4. 由于这个是个 service （系统服务）工程，不能直接debug，需要先将生成在生成目录下（ \DDNSClient\DDNSClient.Service\bin\Debug 或 \DDNSClient\DDNSClient.Service\bin\Release） 的服务安装了才行
5. 点击生成目录下（ \DDNSClient\DDNSClient.Service\bin\Debug 或 \DDNSClient\DDNSClient.Service\bin\Release） 的 InstallService.bat 进行service的安装
6. 点击生成目录下 StartService.bat 启动service服务，也可以在系统的服务中找到 DDNSService 进行启动
7. 此时便会自动获取本机的外网ip,并注册到指定的阿里云域名解析上

# 注意事项
1. 主要的触发代码在 \DDNSClient\DDNSClient.Service\DDNSService.cs，可以在这里面修改触发的时间和找到相应的触发方式
2. 如果需要debug调试，请参考 “使用vs调试Windows service服务”，或者直接参考 https://blog.csdn.net/u012147433/article/details/48730591


