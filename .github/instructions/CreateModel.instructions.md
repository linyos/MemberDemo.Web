---
applyTo: 'Models/*.cs'
---
# Model 層程式碼產生指引

## 專案背景
本專案為 C# 與 Entity Framework Core 的練習專案，Model 層負責定義資料結構，並對應資料庫欄位。

## 設計原則
- 每個 Model 代表一個資料表或資料結構。
- 屬性應對應資料庫欄位，並使用適當的資料型別。
- 支援 Entity Framework Core 的屬性標註（如 [Key], [Required], [MaxLength] 等）。
- Model 應保持單純，不包含商業邏輯。

## 命名慣例
- 類別名稱使用單數且大寫開頭，例如：`User`, `Order`。
- 屬性名稱使用 PascalCase，並清楚描述其用途。

## 資料驗證
- 使用 Data Annotations 進行必要的資料驗證（如 [Required], [StringLength]）。
- 避免在 Model 層進行複雜驗證邏輯。

## 註解
- 為每個 Model 類別與重要屬性加上 XML 註解，說明用途，使用 Zh-TW 語言。

## 其他建議
- 避免在 Model 層加入導覽屬性以外的集合或複雜型別，除非有明確需求。
- 保持 Model 結構簡潔，便於維護與擴充。
- 若有共用欄位（如建立時間、修改時間），可考慮抽象為基底類別繼承。