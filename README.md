# apbd-cw7-git-s26133
# Mini Helpdesk

## Instrukcje

* **Jak uruchomiæ aplikacjê** 
  Otworzyæ rozwi¹zanie w Visual Studio i wcisn¹æ F5 lub w terminalu wpisaæ `dotnet run` w folderze projektu MVC.
* **Jak uruchomiæ testy** 
  W Visual Studio u¿yæ Test Explorer lub w terminalu wpisaæ `dotnet test` w g³ównym folderze rozwi¹zania.
* **U¿yta baza danych** 
  SQLite (plik `helpdesk.db` tworzy siê automatycznie).
* **Gdzie jest middleware** 
  W pliku `Middleware/RequestTimingMiddleware.cs`, zaimplementowany w `Program.cs`.
* **Gdzie jest transakcja** 
  W pliku `Repositories/TicketRepository.cs` (metoda `AddTicketWithCommentAsync`).
* **Gdzie s¹ testy** 
  W osobnym projekcie `MiniHelpdesk.Tests` (testy warstwy Service na bazie implementacji FakeRepository).

---

## Odpowiedzi na pytania

1.  **Dlaczego kolejnoœæ middleware w Program.cs ma znaczenie?**
    Kolejnoœæ determinuje potok (pipeline) przetwarzania ¿¹dania HTTP. Middleware na pocz¹tku potoku (np. logowanie czy obs³uga wyj¹tków) mo¿e przechwyciæ ¿¹danie przed innymi, a tak¿e z³apaæ odpowiedŸ/wyj¹tek, gdy ¿¹danie wraca.
2.  **Czym ró¿ni siê app.Use od app.Run?**
    `app.Use` przekazuje sterowanie do nastêpnego middleware w potoku (wywo³uj¹c `next()`). `app.Run` to middleware koñcowy (terminalny), który nie wywo³uje kolejnych elementów potoku.
3.  **Dlaczego kontroler nie powinien zawieraæ ca³ej logiki aplikacji?**
    Z³ama³oby to zasadê Single Responsibility. Kontroler ma tylko odbieraæ ¿¹dania HTTP, przekazywaæ zadania odpowiedniej warstwie i zwracaæ widok/wynik, co u³atwia testowanie i modyfikacjê logiki biznesowej.
4.  **Co daje test jednostkowy warstwy Service?**
    Pozwala zweryfikowaæ sam¹ logikê biznesow¹ (np. walidacje, zmianê statusów) w izolacji, niezale¿nie od bazy danych czy infrastruktury HTTP, co sprawia, ¿e testy s¹ bardzo szybkie.
5.  **Co powinno siê staæ, jeœli zapis zg³oszenia siê uda, ale zapis komentarza zakoñczy siê b³êdem?**
    Transakcja bazy danych powinna zostaæ wycofana (Rollback). W bazie nie powinno pojawiæ siê ani zg³oszenie, ani komentarz, co gwarantuje spójnoœæ danych.