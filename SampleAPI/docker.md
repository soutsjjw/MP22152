以下指令操作環境皆在`WSL`中。

1. 調整資料庫連線字串。執行下列指令，找到資料庫網址後，修改`appsettings.json`中的連線字串。

	```
	docker network inspect bridge
	```

	[[Docker] Container 之間的溝通，以 APS.NET Core MVC 與 SQL Server 為例](https://mileslin.github.io/2019/04/Container-%E4%B9%8B%E9%96%93%E7%9A%84%E6%BA%9D%E9%80%9A%EF%BC%8C%E4%BB%A5APS-NET-Core-MVC-%E8%88%87-SQL-Server-%E7%82%BA%E4%BE%8B/)


2. 將當前目錄移至`SampleAPI`，執行下列指令建立映像檔：

	```
	docker build -t sampleapi:1.0 .
	```

3. 執行下列指令，將映像檔掛至容器：

	```
	docker run -it --rm -p 5000:5000 sampleapi:1.0
	```