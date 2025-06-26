# ChronoLogic

Hierbei handelt es sich um ein Projekt, welches die Zeiten, die ich in Aktivitäten stecke, die sich sonst schwer zeitlich messen lassen, messbar mache.

## Funktionsumfang

Folgende Funktionen werden vom Projekt abgedeckt

### Benutzerauswahl

Beim Öffnen der Weboberfläche wird der Anwender gefragt, mit welchem Benutzer er sich anmelden möchte oder ob ein neuer angelegt werden soll. Dabei ist aktuell keien Auth gedacht, da es auch eigentlich nicht mehr als einen Benutzer geben sollte. Die Benutzerauswahl ist also als reine Vorschichtsmaßnahme für zukünftige Erweiterungen gedacht.

### Erfassung von Zeiten

Der Anwender soll über eine erste kleine Schaltfläche Zeiten erfassen können, die er für eine Aktivität aufgebracht hat. Bsp:
- Aktivität: Fachbuch lesen
- Kategorie: Weiterbildung
- Beschreibung: (optional)
- Dauer: 2h

### Erfassung von Zeiten mithilfe eines Timers

Wenn der Anwender weiß, dass er sich jetzt gezielt mit einem Thema befassen möchte, kann er statt der gezielten Erfassung auch einen Timer starten lassen. Sobald er den Timer stoppt muss er die Eingaben machen oder diese schon vorher eingegeben haben.

### Erfassung mithilfe von Kategorien

Der Anwender kann Kategorien für seine Aktivitäten hinterlegen. Dies hilft dem System am Ende die Zeiten für diese Kategorien zusammenzurechen. Auch kann über diese Kategorien weitere Felder hinzugefügt werden. Es kann zunächst die Kategorie "Weiterbildung" gewählt werden und anschließend die Kategorie "Fachbuch lesen". Durch die Auswahl von "Fachbuch lesen" wird eine weitere Schaltfläche auf der Oberfläche eingeblendet, die nun Pflicht ist. Die Auswahl des Fachbuchs.

Somit kann am Ende nicht nur bestimmt werden, wie lange sich der Anwender mit persönlicher Weiterbildung befasst hat, sondern auch wie lange er mit einzelnen Fachbüchern gelernt hat.

## Struktur

- ChronoLogic.Api (WEB API)
  - PostgreSql (Persistenz)
- ChronoLogic.Client (Blzaor WASM) 
