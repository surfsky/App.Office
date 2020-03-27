Web 要使用的话，要设置
    <system.web>
		<!-- 
		跳过强名称签名校验(Spire 破解版本用到）
		管理员身份运行 powershell:
		reg ADD "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\StrongName\Verification\*,af24b530b87e22f1" 
		reg ADD "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\StrongName\Verification\*,af24b530b87e22f1" 
		-->
		<hostingEnvironment shadowCopyBinAssemblies="false" />
	</system.web>
	常常会导致dll被锁定，部署时要先清理项目