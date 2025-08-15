# ASP.NET Core API 開發規範  


## Model Class 規範  
1. **使用 DataAnnotations**  
  - 所有 Model 類別必須使用 [DataAnnotations](https://learn.microsoft.com/zh-tw/dotnet/api/system.componentmodel.dataannotations) 進行屬性驗證與描述。  
  - 常用的 DataAnnotations 包括：  
    - `[Required]`：標記為必填欄位。  
    - `[StringLength]`：限制字串長度。  
    - `[Range]`：限制數值範圍。  
    - `[RegularExpression]`：使用正則表達式驗證格式。  
    - `[EmailAddress]`、`[Phone]` 等特殊格式驗證。  


## 命名規範  
1. **DTO 命名**  
  - 所有 Data Transfer Object (DTO) 的類別名稱必須以 `Dto` 結尾，例如：`UserDto`、`OrderDto`。  

2. **郵遞區號欄位**  
  - 若需要產生郵遞區號的欄位，請固定使用 5 至 6 個字元的字串格式，並命名為 `PostalCode`。  


2. **範例**
## API 設計規範  
1. **RESTful API**  
   - 遵循 RESTful API 設計原則，使用 HTTP 方法表示操作：  
     - `GET`：讀取資源。  
     - `POST`：建立資源。  
     - `PUT`：更新資源。  
     - `DELETE`：刪除資源。  

2. **路由設計**  
   - 使用簡潔且具描述性的路由，例如：  
     - `GET /api/users`：取得所有使用者。  
     - `GET /api/users/{id}`：取得特定使用者。  
     - `POST /api/users`：新增使用者。  
     - `PUT /api/users/{id}`：更新使用者。  
     - `DELETE /api/users/{id}`：刪除使用者。  

3. **回應格式**  
   - API 回應應使用標準的 JSON 格式，並包含以下結構：
4. **錯誤處理**  
   - 使用一致的錯誤回應格式，例如：
## 測試規範  
1. **單元測試**  
   - 所有 API 必須撰寫單元測試，覆蓋率應達到 80% 以上。  
   - 使用 `xUnit` 或其他支援 .NET 的測試框架。  

2. **整合測試**  
   - 撰寫整合測試以驗證 API 的整體行為。  

3. **範例測試類別**
## 版本控制  
1. **API 版本**  
   - 使用 URL 路徑進行版本控制，例如：`/api/v1/users`。  

2. **版本升級**  
   - 當 API 發生重大變更時，應升級版本並保持向後相容性。  

## 文件與註解  
1. **Swagger 文件**  
   - 使用 [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) 或其他工具自動生成 API 文件。  

2. **程式碼註解**  
   - 使用 XML 註解描述類別與方法，方便生成文件。