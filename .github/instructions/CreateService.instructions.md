---
applyTo: '**/Service.cs'
---
# Service 層程式碼產生指引

## 專案背景
本專案為 C# 與 Entity Framework Core 的練習專案，目標為實作標準的 CRUD 與商業邏輯，並維持良好架構與可維護性。

## 設計原則
- Service 加上註解，描述其功能與用途，使用Zh-TW語言。
- 採用單一職責原則（SRP），每個 Service 僅負責一個業務領域。
- Service 不直接操作資料庫，應透過 Repository 或 DbContext。
- 支援依賴注入（Dependency Injection）。
- 所有公開方法皆應為非同步（async/await）。

## 命名慣例
- Service 類別名稱以 `Service` 結尾，例如：`UserService`。
- 方法名稱以動詞開頭，清楚描述行為，例如：`GetUserByIdAsync`、`CreateOrderAsync`。

## 錯誤處理
- 捕捉例外時，記錄詳細錯誤資訊，並回傳自訂例外或標準例外。
- 不要吞掉例外，必要時重新拋出。

## 測試建議
- 為每個 Service 方法撰寫單元測試。
- 測試應涵蓋正常流程與異常情境。

## 其他建議
- 方法參數與回傳型別應明確，不使用 dynamic 或 object。
- 避免在 Service 層進行資料驗證，應於上層（如 Controller）處理。
- 保持程式碼簡潔，適當拆分複雜邏輯至私有方法。