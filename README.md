# OrderProcessingApp

Projekt został stworzony w ramach zadania rekrutacyjnego i rozszerzony o dodatkowe funkcjonalności, takie jak historia statusów zamówień oraz testy logiki biznesowej.

## Dodatkowe funkcjonalności
Aplikacja została wzbogacona o:

* InMemoryDatabase: Implementację bazy danych z podejściem Code-First działającą w pamięci.

* Historię statusów zamówień: Każda zmiana statusu zamówienia jest przechowywana, co pozwala na śledzenie historii procesu.

* Testy logiki biznesowej: Pokrywają zasady biznesowe oraz proces zmiany statusów zamówień.

## Rozwiązania obiektowe
W projekcie zastosowano podejście obiektowe, w tym:

* Adres: Klasa reprezentująca adres dostawy.

* Zamówienie: Klasa opisująca właściwości zamówienia.

* Produkt: Klasa przechowująca nazwę produktu.

* Kwota zamówienia: Klasa reprezentująca wartość zamówienia wraz z obsługą waluty jako zagnieżdżonym obiektem.

* Zasady biznesowe: Każda zasada biznesowa została zaimplementowana jako osobna klasa (OrderBusinessRules).

* Dodatkowo typy takie jak Typ Klienta, Sposób płatności oraz Status Zamówienia zostały przedstawione jako enum.

## Architektura aplikacji
* UserInputService: Klasa odpowiedzialna za interakcję z użytkownikiem. Przechowuje wszystkie wiadomości systemowe w formie prywatnych pól, co umożliwia łatwą modyfikację i potencjalną lokalizację aplikacji w przyszłości.

* OrderService: Klasa pośrednicząca między UserInputService a OrderRepository. Zawiera logiki biznesowe oraz zapisu i odczytu danych.

* Fabryka zamówień (OrderFactory): Wykorzystano wzorzec projektowy fabryki (OrderFactory) w celu uproszczenia procesu tworzenia zamówień.
