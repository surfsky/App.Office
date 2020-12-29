# About

Transfer files to PDF
surfsky.github.com
	

# Supports

- [x] Word
- [x] PowerPoint
- [x] Excel
- [x] Text
- [x] Markdown
- [x] Html



# Notice

Asp.net Web need to modify web.config file:

``` xml
<system.web>
	<!-- 
	跳过强名称签名校验(Spire 破解版本用到）
	管理员身份运行 powershell:
	reg ADD "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\StrongName\Verification\*,af24b530b87e22f1" 
	reg ADD "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\StrongName\Verification\*,af24b530b87e22f1" 
	-->
	<hostingEnvironment shadowCopyBinAssemblies="false" />
</system.web>
```

	但该操作会导致dll被锁定，部署时要先清理项目
