# Car Rental System


Projekt zaliczeniowy

Zaawansowane Interfejsy Graficzne

Cezary Zięba, Jan Zoń

2024


## Zagadnienie biznesowe


Zadaniem projektowym było stworzenie aplikacji-systemu dla wypożyczalni 
samochodów. System miał obejmować zarówno część interfejsu dostępną dla 
potencjalnych klientów, jak i część dla pracowników wypożyczalni.


## Funkcjonalności


- Okno logowania/rejestracji - pozwala pracownikom/klientom zalogować się 
na swoje konto lub pozwala klientom takowe utworzyć. Aby się zalogować należy 
w odpowiednie pola tekstowe z lewej strony okna wprowadzić e-mail oraz hasło, 
a do rejestracji (po przeciwnej stronie) - poza już wymienionymi - imię, 
nazwisko, numer telefonu, adres, numer karty kredytowej, data ważności karty, 
numer CVV.
- Okno dla klientów - umożliwia klientom przegląd bazy aut wypożyczalni oraz 
ewentualne dokonanie wynajmu. Okno podzielone zostało na 3 sekcje: lista 
wypożyczonych aut, lista dostępnych aut (do wypożyczenia), lista niedostępnych 
aut. Wypożyczanie auta odbywa się poprzez nacisnięcie przez klienta przycisku 
przy dostępnym pojeździe, co skutkuje pokazaniem się okna z możliwością wyboru 
daty końca wynajmu oraz potwierdzeniem wynajmu za wyświetloną cenę.
- Okno dla pracowników - pozwala managerom/mechanikom na zarządzanie 
składnikami systemu. Manager może dodawać nowe pojazdy, edytować dane 
istniejących, usuwać pojazdy, tworzyć konta innych pracowników, edytować dane 
kont innych pracowników, usuwać konta pracowników, usuwać konta klientów, 
sprawdzać historię wynajmu danego pojazdu. Mechanik natomiast może wprowadzić 
auto w stan serwisowania, sprawdzić historię serwisowania danego pojazdu, 
sprawdzić historię wynajmu danego pojazdu oraz zakończyć serwisowanie pojazdu. 
Każda z wymienionych funkcjonalności wymaga wprowadzenia danych/zedytowania 
danych w odpowiednich polach i potwierdzenia operacji przyciskiem lub też  
wybrania z rozwijanej listy interesującego elementu.


## Charakterystyka technologiczna


- Visual Studio 2022
- .NET Framework 4.7.2
- relacyjna baza danych (SQLite)

