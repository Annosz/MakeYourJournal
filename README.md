# MakeYourJournal
A MakeYourJournal egy olyan to-do alkalmazás, ami speciálisan az újságírók segítésére készült, és alkalmazható egy kisebb szerkesztőség munkájának a vezetéséhez.
A főszerkesztő az aktuális számhoz létrehozhat egy oldal a lapzárta idejével együtt, ahova a szerkesztők felvehetik a saját feladataikat (a megírandó cikkeket), kisebb részfeladatokkal és jegyzetekkel együtt. Részfeladatonként jelezhető a haladás, és a végén a főszerkesztő elfogadhatja a cikket, valamint lapzártánál lezárhatja a szám oldalát.

## A kisháziban elérhető funkciók
* A főszerkesztő új oldalt hozhat létre az aktuális számnak, a lapzárta megadásával
* A felhasználók felvehetik a megírandó cikkeket, és módosíthatják vagy törölhetik azokat.
* A felhasználók részfeladatokat vagy jegyzeteket vehetnek fel a cikkek megírásához
* A felhasználók elvégzetnek jelölhetnek egy részfeladatot
* A főszerkesztő elfogadhat egy cikket és lezárhatja a szám oldalát a lapzárta elteltével

## Alkalmazott alaptechnológiák
* Adatelérés: Entity Framework Core
* Kommunikáció: ASP.NET Core
* Felület: Angular 4
