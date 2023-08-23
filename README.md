# Core_Mvc6_Restaurant_Init
Azure網址: https://restaurantappservice.azurewebsites.net/
測試帳號: test123admin
測試密碼: admin123test

一. 專案想法 (Side_Project ideas)
  1. 2023/7月開始開發 
  2. 主要目的是自學實作一些2022/10月轉職班畢業後，開始求職時不會的後端功能，Ex: 使用Microsoft.AspNetCore.Identity實作登入驗證, 簡易Github Action部署上Azure App Service, 使用Azure Sql Server並讓app在Dev環境使用User Secrets連線字串的設定檔&Production環境使用Azure App Configuration的連線字串
  3. 要讓在經營小吃攤的朋友(人在台中)可以實際用在他的店面，自己維護菜單上的菜名/價格/圖片，而且可以有活動區塊可以吸引到用google map找美食的顧客，增加顧客上門的機率

     Overall into English, this project started since July, 2023. The main idea is oriented to a thought to develop a website for a friend who runs a small food shop.
  In terms of support, the website should consist a backstage admin page for the owner to manage his own dish menu. This shall consists of name, photography, and price of 
  each dish the owner wish to display on the website. In addition, information of monthly activity inside shop could be shown on the website as well. Hopefully it can make 
  it to attract tourists who wish to find a fine place for a leisure meal break.
二. 使用技術 (Practiced TechPack)
  1. 前端技術(Frontend)
     
     (a) BootStrap
     (b) Native JavaScript
     (c) Vue.js (Maybe used)
  3. 資料庫(Database):

     (a) 設計後台表格
  4. 後端技術(Backend)
     
     (a) .Net Core 6 MVC with Clean Architecture => Repository/Services/Views
     (b) .Net Core 6 Entity Framework
  5. 雲端部署DevOps:
     
     (a) Github CI/CD Automated Publish to Azure
     (b) Azure Sql Server and database
     (c) Azure App Service

Last updated: 2023/7/23 by Leo

2023/8/24 更新 by Leo
1. 這次想要實作JwtToken的功能，於是乎我先開了一個WebAPI的專案，並且deploy到Azure Web Service，專案同樣運用Entity Framework Core來做簡易驗證使用者帳密，並且產生jwt token回傳的POST API。
2. 在本專案將Cookies驗證改成，使用者輸入帳密之後，得到jwt token，但是在進入後台前，仍然要先解析jwt的expire time是否有效，如果失效還是要重新登入，如果jwt token有效則無需登入可直接進入後台直到失效or使用者登出。
