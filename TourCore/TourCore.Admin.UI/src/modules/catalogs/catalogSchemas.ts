export type FieldKind='text'|'number'|'boolean'|'date'|'time'|'textarea'
export interface CatalogField { name:string; label:string; kind?:FieldKind; nullable?:boolean }
export interface CatalogSchema { endpoint:string; fields:CatalogField[]; filters:CatalogField[]; columns?:string[] }
export interface LookupSpec { endpoint:string; valueKey?:string; labelKey?:string; dependsOn?:string; filterName?:string }

const byId=(endpoint:string,dependsOn?:string,filterName?:string):LookupSpec=>({endpoint,dependsOn,filterName})
export function lookupFor(fieldName:string,availableFields:string[]):LookupSpec|undefined {
 const direct:Record<string,LookupSpec>={
  ParentId:byId('/api/v1/tours/tour-types'),CountryId:byId('/api/v1/geography/countries'),CountryFromId:byId('/api/v1/geography/countries'),CountryToId:byId('/api/v1/geography/countries'),
  RegionId:byId('/api/v1/geography/regions',availableFields.includes('CountryId')?'CountryId':undefined,'CountryId'),CityId:byId('/api/v1/geography/cities',availableFields.includes('CountryId')?'CountryId':undefined,'CountryId'),CityFromId:byId('/api/v1/geography/cities',availableFields.includes('CountryFromId')?'CountryFromId':undefined,'CountryId'),CityToId:byId('/api/v1/geography/cities',availableFields.includes('CountryToId')?'CountryToId':undefined,'CountryId'),
  DepartureCityId:byId('/api/v1/geography/cities'),ArrivalCityId:byId('/api/v1/geography/cities'),ResortId:byId('/api/v1/geography/resorts',availableFields.includes('CountryId')?'CountryId':undefined,'CountryId'),CategoryId:byId('/api/v1/hotels/hotel-categories'),
  MainPlacementRuleId:byId('/api/v1/hotels/accommodation-placement-rules'),ExtraPlacementRuleId:byId('/api/v1/hotels/accommodation-placement-rules'),CharterId:byId('/api/v1/avia/charters'),DirectionId:byId('/api/v1/transfers/directions'),TransportId:byId('/api/v1/transportation/transports'),
  BusTransferId:byId('/api/v1/bus/transfers'),RailwayTransferId:byId('/api/v1/railway/transfers'),VehiclePlanId:byId('/api/v1/seating/vehicle-plans'),
  DepartureAirportCode:{endpoint:'/api/v1/avia/airports',valueKey:'Code',dependsOn:'DepartureCityId',filterName:'CityId'},ArrivalAirportCode:{endpoint:'/api/v1/avia/airports',valueKey:'Code',dependsOn:'ArrivalCityId',filterName:'CityId'},AirlineCode:{endpoint:'/api/v1/avia/airlines',valueKey:'Code'},AircraftCode:{endpoint:'/api/v1/avia/aircrafts',valueKey:'Code'},AirClassCode:{endpoint:'/api/v1/avia/air-classes',valueKey:'Code'},
  FromRateCode:{endpoint:'/api/v1/finance/rates',valueKey:'Code'},ToRateCode:{endpoint:'/api/v1/finance/rates',valueKey:'Code'},
 }
 return direct[fieldName]
}

const labels:Record<string,string>={Id:'ID',Name:'Название',NameEn:'Название (англ.)',Code:'Код',CountryId:'Страна',RegionId:'Регион',CityId:'Город',ResortId:'Курорт',CategoryId:'Категория',SortOrder:'Порядок',ShowOrder:'Порядок',Description:'Описание',Search:'Поиск',IsDeparturePoint:'Город вылета',IcaoCode:'ICAO',IataCode:'IATA',IsoCode:'ISO-код',IsoCode2:'ISO-2',IsoCode3:'ISO-3',GlobalCode:'Глобальный код',IsMain:'Основной',IsNational:'Национальная',ShowInSearch:'Показывать в поиске',DateFrom:'Дата с',DateTo:'Дата по',TimeFrom:'Время с',TimeTo:'Время по',CountryFromId:'Страна отправления',CityFromId:'Город отправления',CountryToId:'Страна прибытия',CityToId:'Город прибытия',DaysOfWeek:'Дни недели',Remark:'Примечание',Comment:'Комментарий'}
export const fieldLabel=(name:string)=>labels[name]??name
const booleans=new Set(['IsDeparturePoint','IsCruise','IsMain','IsNational','ShowInSearch','IsDuration','IsRoute','AutoApplyFrom','AutoApplyTo','PlanOrientation','IsAircraft','ChildrenAreInfants','IsNextDayArrival'])
const dates=new Set(['DateFrom','DateTo','DateBeg','DateEnd'])
const times=new Set(['TimeFrom','TimeTo'])
const textareas=new Set(['Description','Remark','Comment','Address','Border'])
const numeric=/^(Id|.*Id|.*Count|.*Order|Rank|Places|ExtraPlaces|AgeFrom|AgeTo|PerRoom|AreaNumber|RowsCount|ColumnsCount|Index|DayFrom|DayTo|Course|CentralBankCourse)$/
const f=(names:string[]):CatalogField[]=>names.map(name=>({name,label:fieldLabel(name),kind:booleans.has(name)?'boolean':dates.has(name)?'date':times.has(name)?'time':textareas.has(name)?'textarea':numeric.test(name)?'number':'text',nullable:name.endsWith('Id')||name.startsWith('Date')||name.startsWith('Time')}))
const schema=(endpoint:string,fields:string[],filters:string[]=['Search'],columns?:string[]):CatalogSchema=>({endpoint,fields:f(fields),filters:f(filters),columns})
const named=(endpoint:string,extra:string[]=[],filters:string[]=['Search'])=>schema(endpoint,['Code','Name','NameEn',...extra],filters)

export const catalogSchemas:Record<string,CatalogSchema>={
 'tour-types':schema('/api/v1/tours/tour-types',['Name','ParentId'],['Search','ParentId']),
 countries:schema('/api/v1/geography/countries',['Name','NameEn','Code','IsoCode2','IsoCode3','SortOrder','DigitalCode','CitizenshipName','CitizenshipNameEn']),
 regions:schema('/api/v1/geography/regions',['CountryId','Name','NameEn','Code','SortOrder'],['Search','CountryId']),
 cities:schema('/api/v1/geography/cities',['CountryId','RegionId','Name','NameEn','Code','SortOrder','IsDeparturePoint','TimeZone','IataCode','Coordinates'],['Search','CountryId','RegionId','IsDeparturePoint']),
 resorts:schema('/api/v1/geography/resorts',['CountryId','Name','NameEn'],['Search','CountryId']),
 hotels:schema('/api/v1/hotels',['CountryId','CityId','ResortId','CategoryId','Name','NameEn','Stars','Code','Address','Phone','Fax','Email','Website','Latitude','Longitude','IsCruise','SortOrder','Rank'],['Search','CountryId','CityId','ResortId','CategoryId','IsCruise']),
 'hotel-categories':schema('/api/v1/hotels/hotel-categories',['Name','NameEn','PrintOrder','GlobalCode','Description']),
 'room-types':named('/api/v1/hotels/room-types',['Places','ExtraPlaces','SortOrder','Description']),
 'room-categories':named('/api/v1/hotels/room-categories',['SortOrder','Description']),
 'meal-types':named('/api/v1/hotels/meal-types',['GlobalCode','SortOrder','Description'],['Search','GlobalCode']),
 'accommodation-types':named('/api/v1/hotels/accommodation-types',['IsMain','AgeFrom','AgeTo','PerRoom','SortOrder','Description','MainPlacementRuleId','ExtraPlacementRuleId'],['Search','IsMain']),
 'accommodation-rules':schema('/api/v1/hotels/accommodation-placement-rules',['AdultsCount','ChildrenCount','ChildrenAreInfants'],['AdultsCount','ChildrenCount','ChildrenAreInfants']),
 airlines:named('/api/v1/avia/airlines',['IcaoCode'],['Search','IcaoCode']),
 aircraft:named('/api/v1/avia/aircrafts'),
 airports:named('/api/v1/avia/airports',['CityId','IcaoCode','LetterCode'],['Search','CityId','IcaoCode']),
 'air-classes':named('/api/v1/avia/air-classes',['Group','SortOrder'],['Search','Group']),
 charters:schema('/api/v1/avia/charters',['DepartureCityId','DepartureAirportCode','ArrivalCityId','ArrivalAirportCode','AirlineCode','FlightNumber','AircraftCode','AirClassCode','StopsCount','TimeChangesCode'],['DepartureCityId','ArrivalCityId','DepartureAirportCode','ArrivalAirportCode','AirlineCode','FlightNumber']),
 'charter-seasons':schema('/api/v1/avia/charter-seasons',['CharterId','DateFrom','DateTo','DaysOfWeek','TimeFrom','TimeTo','IsNextDayArrival','Remark'],['CharterId','DateFrom','DateTo']),
 'service-types':named('/api/v1/services/service-types',['IsDuration','IsRoute'],['Search','IsDuration','IsRoute']),
 transfers:schema('/api/v1/transfers',['Name','NameEn','TimeFrom','TimeTo','DurationText','PlaceFrom','PlaceTo','IsMain','CityId','DirectionId','Url','ShowOrder','AutoApplyFrom','AutoApplyTo'],['Search','CityId','DirectionId','IsMain']),
 'transfer-directions':schema('/api/v1/transfers/directions',['Name']),
 transports:schema('/api/v1/transportation/transports',['Name','NameEn','SeatsCount','ShowOrder']),
 'bus-transfers':schema('/api/v1/bus/transfers',['Name','CountryFromId','CityFromId','CountryToId','CityToId'],['Search','CountryFromId','CityFromId','CountryToId','CityToId']),
 'bus-transfer-points':schema('/api/v1/bus/transfer-points',['BusTransferId','CountryFromId','CityFromId','CountryToId','CityToId','TimeFrom','TimeTo','DayFrom','DayTo'],['BusTransferId','CountryFromId','CityFromId','CountryToId','CityToId']),
 'bus-schedules':schema('/api/v1/bus/schedules',['BusTransferId','DateFrom','DateTo','TimeFrom','TimeTo','DaysOfWeek','DaysOnRoad'],['BusTransferId']),
 'railway-transfers':schema('/api/v1/railway/transfers',['Name','CountryFromId','CityFromId','CountryToId','CityToId'],['Search','CountryFromId','CityFromId','CountryToId','CityToId']),
 'train-schedules':schema('/api/v1/railway/schedules',['RailwayTransferId','DateFrom','DateTo','TimeFrom','TimeTo','DaysOfWeek','DaysOnRoad','Remark'],['RailwayTransferId']),
 'vehicle-plans':schema('/api/v1/seating/vehicle-plans',['TransportId','RowsCount','ColumnsCount','AreaNumber','Name','PlanOrientation','IsAircraft','Dates','Comment'],['TransportId','IsAircraft','Search']),
 'seating-cells':schema('/api/v1/seating/cells',['VehiclePlanId','Number','Type','SeatsCount','Index','Border'],['VehiclePlanId','Type','Number']),
 rates:schema('/api/v1/finance/rates',['Code','Name','IsoCode','IsMain','IsNational','ShowInSearch'],['Search','IsMain','IsNational','ShowInSearch']),
 'real-courses':schema('/api/v1/finance/real-courses',['FromRateCode','ToRateCode','Course','CentralBankCourse','DateBeg','DateEnd'],['FromRateCode','ToRateCode','DateFrom','DateTo']),
}
