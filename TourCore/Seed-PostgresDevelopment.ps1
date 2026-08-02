param(
    [string]$ApiBaseUrl = "http://localhost:5132"
)

$ErrorActionPreference = "Stop"

function Invoke-AdminPost {
    param(
        [Parameter(Mandatory)] [string]$Path,
        [Parameter(Mandatory)] [hashtable]$Body
    )

    Invoke-RestMethod `
        -Method Post `
        -Uri "$ApiBaseUrl$Path" `
        -ContentType "application/json; charset=utf-8" `
        -Body ($Body | ConvertTo-Json -Depth 10)
}

$existingCountries = Invoke-RestMethod -Uri "$ApiBaseUrl/api/v1/geography/countries?page=1&pageSize=1"
if ($existingCountries.TotalCount -gt 0) {
    throw "PostgreSQL development database is not empty. Seeding was cancelled to prevent duplicates."
}

Write-Host "Creating geography..."
$russia = Invoke-AdminPost "/api/v1/geography/countries" @{
    Name="Россия"; NameEn="Russia"; Code="RUS"; IsoCode2="RU"; IsoCode3="RUS";
    DigitalCode="643"; CitizenshipName="Россиянин"; CitizenshipNameEn="Russian"; SortOrder=10
}
$turkey = Invoke-AdminPost "/api/v1/geography/countries" @{
    Name="Турция"; NameEn="Turkey"; Code="TUR"; IsoCode2="TR"; IsoCode3="TUR";
    DigitalCode="792"; CitizenshipName="Турок"; CitizenshipNameEn="Turkish"; SortOrder=20
}
$georgia = Invoke-AdminPost "/api/v1/geography/countries" @{
    Name="Грузия"; NameEn="Georgia"; Code="GEO"; IsoCode2="GE"; IsoCode3="GEO";
    DigitalCode="268"; CitizenshipName="Грузин"; CitizenshipNameEn="Georgian"; SortOrder=30
}

$moscowRegion = Invoke-AdminPost "/api/v1/geography/regions" @{
    CountryId=$russia.Id; Name="Московский регион"; NameEn="Moscow Region"; Code="MSK"; SortOrder=10
}
$antalyaRegion = Invoke-AdminPost "/api/v1/geography/regions" @{
    CountryId=$turkey.Id; Name="Анталья"; NameEn="Antalya"; Code="ANT"; SortOrder=10
}
$adjara = Invoke-AdminPost "/api/v1/geography/regions" @{
    CountryId=$georgia.Id; Name="Аджария"; NameEn="Adjara"; Code="ADJ"; SortOrder=10
}

$moscow = Invoke-AdminPost "/api/v1/geography/cities" @{
    CountryId=$russia.Id; RegionId=$moscowRegion.Id; Name="Москва"; NameEn="Moscow"; Code="MOW";
    IataCode="MOW"; TimeZone="Europe/Moscow"; Coordinates="55.7558,37.6173"; SortOrder=10; IsDeparturePoint=$true
}
$petersburg = Invoke-AdminPost "/api/v1/geography/cities" @{
    CountryId=$russia.Id; Name="Санкт-Петербург"; NameEn="Saint Petersburg"; Code="LED";
    IataCode="LED"; TimeZone="Europe/Moscow"; Coordinates="59.9343,30.3351"; SortOrder=20; IsDeparturePoint=$true
}
$antalya = Invoke-AdminPost "/api/v1/geography/cities" @{
    CountryId=$turkey.Id; RegionId=$antalyaRegion.Id; Name="Анталья"; NameEn="Antalya"; Code="AYT";
    IataCode="AYT"; TimeZone="Europe/Istanbul"; Coordinates="36.8969,30.7133"; SortOrder=10; IsDeparturePoint=$false
}
$alanya = Invoke-AdminPost "/api/v1/geography/cities" @{
    CountryId=$turkey.Id; RegionId=$antalyaRegion.Id; Name="Аланья"; NameEn="Alanya"; Code="ALA";
    TimeZone="Europe/Istanbul"; Coordinates="36.5444,31.9954"; SortOrder=20; IsDeparturePoint=$false
}
$batumi = Invoke-AdminPost "/api/v1/geography/cities" @{
    CountryId=$georgia.Id; RegionId=$adjara.Id; Name="Батуми"; NameEn="Batumi"; Code="BUS";
    IataCode="BUS"; TimeZone="Asia/Tbilisi"; Coordinates="41.6168,41.6367"; SortOrder=10; IsDeparturePoint=$false
}

$antalyaResort = Invoke-AdminPost "/api/v1/geography/resorts" @{ CountryId=$turkey.Id; Name="Анталийское побережье"; NameEn="Antalya Coast" }
$alanyaResort = Invoke-AdminPost "/api/v1/geography/resorts" @{ CountryId=$turkey.Id; Name="Аланийское побережье"; NameEn="Alanya Coast" }
$batumiResort = Invoke-AdminPost "/api/v1/geography/resorts" @{ CountryId=$georgia.Id; Name="Батуми"; NameEn="Batumi" }

Write-Host "Creating hotel dictionaries and hotels..."
$fiveStars = Invoke-AdminPost "/api/v1/hotels/hotel-categories" @{ Name="5 звезд"; NameEn="5 stars"; PrintOrder=10; GlobalCode="5STAR"; Description="Отели категории 5 звезд" }
$fourStars = Invoke-AdminPost "/api/v1/hotels/hotel-categories" @{ Name="4 звезды"; NameEn="4 stars"; PrintOrder=20; GlobalCode="4STAR"; Description="Отели категории 4 звезды" }
Invoke-AdminPost "/api/v1/hotels/room-types" @{ Code="DBL"; Name="Двухместный"; NameEn="Double"; Places=2; ExtraPlaces=1; SortOrder=10; Description="Стандартный двухместный номер" } | Out-Null
Invoke-AdminPost "/api/v1/hotels/room-types" @{ Code="FAM"; Name="Семейный"; NameEn="Family"; Places=3; ExtraPlaces=1; SortOrder=20; Description="Семейный номер" } | Out-Null
Invoke-AdminPost "/api/v1/hotels/room-categories" @{ Code="STD"; Name="Стандарт"; NameEn="Standard"; SortOrder=10; Description="Стандартная категория" } | Out-Null
Invoke-AdminPost "/api/v1/hotels/room-categories" @{ Code="SEA"; Name="Вид на море"; NameEn="Sea View"; SortOrder=20; Description="Номер с видом на море" } | Out-Null
Invoke-AdminPost "/api/v1/hotels/meal-types" @{ Code="BB"; GlobalCode="BB"; Name="Завтраки"; NameEn="Bed and Breakfast"; SortOrder=10; Description="Завтрак включён" } | Out-Null
Invoke-AdminPost "/api/v1/hotels/meal-types" @{ Code="AI"; GlobalCode="AI"; Name="Всё включено"; NameEn="All Inclusive"; SortOrder=20; Description="Всё включено" } | Out-Null

$hotels = @(
    @{ CountryId=$turkey.Id; CityId=$antalya.Id; ResortId=$antalyaResort.Id; CategoryId=$fiveStars.Id; Name="Azure Bay Antalya"; NameEn="Azure Bay Antalya"; Stars="5"; Code="AZBAY"; Address="Lara, Antalya"; Latitude="36.8520"; Longitude="30.8010"; IsCruise=$false; SortOrder=10; Rank=95 },
    @{ CountryId=$turkey.Id; CityId=$antalya.Id; ResortId=$antalyaResort.Id; CategoryId=$fourStars.Id; Name="Old Town Garden"; NameEn="Old Town Garden"; Stars="4"; Code="OTGRD"; Address="Kaleici, Antalya"; Latitude="36.8841"; Longitude="30.7056"; IsCruise=$false; SortOrder=20; Rank=85 },
    @{ CountryId=$turkey.Id; CityId=$alanya.Id; ResortId=$alanyaResort.Id; CategoryId=$fiveStars.Id; Name="Mediterranean Sun"; NameEn="Mediterranean Sun"; Stars="5"; Code="MEDSN"; Address="Alanya Coast"; Latitude="36.5438"; Longitude="31.9998"; IsCruise=$false; SortOrder=30; Rank=92 },
    @{ CountryId=$georgia.Id; CityId=$batumi.Id; ResortId=$batumiResort.Id; CategoryId=$fiveStars.Id; Name="Batumi Wave"; NameEn="Batumi Wave"; Stars="5"; Code="BTWAV"; Address="Batumi Boulevard"; Latitude="41.6501"; Longitude="41.6360"; IsCruise=$false; SortOrder=10; Rank=93 },
    @{ CountryId=$georgia.Id; CityId=$batumi.Id; ResortId=$batumiResort.Id; CategoryId=$fourStars.Id; Name="Adjara Garden"; NameEn="Adjara Garden"; Stars="4"; Code="ADGRD"; Address="Old Batumi"; Latitude="41.6434"; Longitude="41.6339"; IsCruise=$false; SortOrder=20; Rank=82 }
)
foreach ($hotel in $hotels) { Invoke-AdminPost "/api/v1/hotels" $hotel | Out-Null }

Write-Host "Creating finance and transport dictionaries..."
Invoke-AdminPost "/api/v1/finance/rates" @{ Code="RUB"; Name="Российский рубль"; IsoCode="RUB"; IsMain=$true; IsNational=$true; ShowInSearch=$true } | Out-Null
Invoke-AdminPost "/api/v1/finance/rates" @{ Code="EUR"; Name="Евро"; IsoCode="EUR"; IsMain=$false; IsNational=$false; ShowInSearch=$true } | Out-Null
$airTransport = Invoke-AdminPost "/api/v1/transportation/transports" @{ Name="Самолёт"; NameEn="Aircraft"; SeatsCount=180; ShowOrder=10 }
$busTransport = Invoke-AdminPost "/api/v1/transportation/transports" @{ Name="Автобус"; NameEn="Bus"; SeatsCount=50; ShowOrder=20 }
$railTransport = Invoke-AdminPost "/api/v1/transportation/transports" @{ Name="Поезд"; NameEn="Train"; SeatsCount=250; ShowOrder=30 }

$economy = Invoke-AdminPost "/api/v1/avia/air-classes" @{ Code="Y"; Name="Эконом"; NameEn="Economy"; Group="Economy"; SortOrder=10 }
$aircraft = Invoke-AdminPost "/api/v1/avia/aircrafts" @{ Code="320"; Name="Airbus A320"; NameEn="Airbus A320" }
$airline = Invoke-AdminPost "/api/v1/avia/airlines" @{ Code="TC"; Name="TourCore Airlines"; NameEn="TourCore Airlines"; IcaoCode="TCR" }
Invoke-AdminPost "/api/v1/avia/airports" @{ CityId=$moscow.Id; Code="VKO"; Name="Внуково"; NameEn="Vnukovo"; IcaoCode="UUWW"; LetterCode="V" } | Out-Null
Invoke-AdminPost "/api/v1/avia/airports" @{ CityId=$antalya.Id; Code="AYT"; Name="Аэропорт Анталья"; NameEn="Antalya Airport"; IcaoCode="LTAI"; LetterCode="A" } | Out-Null

Write-Host "Creating search transport scenarios..."
$charter = Invoke-AdminPost "/api/v1/avia/charters" @{
    DepartureCityId=$moscow.Id; DepartureAirportCode="VKO"; ArrivalCityId=$antalya.Id; ArrivalAirportCode="AYT";
    AirlineCode=$airline.Code; FlightNumber="TC01"; AircraftCode=$aircraft.Code; AirClassCode=$economy.Code; StopsCount=0
}
Invoke-AdminPost "/api/v1/avia/charter-seasons" @{
    CharterId=$charter.Id; DateFrom="2026-08-01T00:00:00Z"; DateTo="2027-10-31T00:00:00Z";
    DaysOfWeek="1234567"; TimeFrom="2026-08-01T07:00:00Z"; TimeTo="2026-08-01T11:30:00Z"; IsNextDayArrival=$false; Remark="Ежедневно"
} | Out-Null

$bus = Invoke-AdminPost "/api/v1/bus/transfers" @{
    Name="Москва — Батуми"; CountryFromId=$russia.Id; CityFromId=$moscow.Id; CountryToId=$georgia.Id; CityToId=$batumi.Id
}
Invoke-AdminPost "/api/v1/bus/schedules" @{
    BusTransferId=$bus.Id; DateFrom="2026-08-01T00:00:00Z"; DateTo="2027-10-31T00:00:00Z";
    TimeFrom="2026-08-01T08:00:00Z"; TimeTo="2026-08-02T18:00:00Z"; DaysOfWeek="1357"; DaysOnRoad=2
} | Out-Null

$rail = Invoke-AdminPost "/api/v1/railway/transfers" @{
    Name="Санкт-Петербург — Батуми"; CountryFromId=$russia.Id; CityFromId=$petersburg.Id; CountryToId=$georgia.Id; CityToId=$batumi.Id
}
Invoke-AdminPost "/api/v1/railway/schedules" @{
    RailwayTransferId=$rail.Id; DateFrom="2026-08-01T00:00:00Z"; DateTo="2027-10-31T00:00:00Z";
    TimeFrom="2026-08-01T20:00:00Z"; TimeTo="2026-08-03T10:00:00Z"; DaysOfWeek="246"; DaysOnRoad=3; Remark="Учебный маршрут"
} | Out-Null

$arrivalDirection = Invoke-AdminPost "/api/v1/transfers/directions" @{ Name="Аэропорт — отель" }
Invoke-AdminPost "/api/v1/transfers" @{
    Name="Групповой трансфер Анталья"; NameEn="Antalya group transfer"; DurationText="01:00";
    PlaceFrom="Аэропорт Анталья"; PlaceTo="Отели Антальи"; IsMain=$true; CityId=$antalya.Id;
    DirectionId=$arrivalDirection.Id; ShowOrder=10; AutoApplyFrom=$true; AutoApplyTo=$false
} | Out-Null

Write-Host "PostgreSQL development data created successfully."
