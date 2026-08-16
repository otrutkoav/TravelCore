export interface PagedResponse<T> { Items: T[]; Page: number; PageSize: number; TotalCount: number }
export interface ValidationErrorItem { Code: string; Message: string }
export interface ApiErrorResponse { Code: string; Message: string; Details?: Record<string, ValidationErrorItem[]> }
export interface TourListItemDto { Id:number; Name:string; Status:number; Revision:number; CurrencyId:number; DateMode:number; AvailableDateFrom?:string; AvailableDateTo?:string }
export interface TourTypeListItemDto { Id:number; Name:string; ParentId?:number }
export interface TourDto extends TourListItemDto { AvailableDates:string[]; TourTypes:TourTypeListItemDto[]; DescriptionUrl?:string; DescriptionHtml?:string; BookingConditionsHtml?:string; CreatedAt:string; UpdatedAt?:string }
export interface TourCommand { Name:string; CurrencyId:number; DateMode:number; AvailableDateFrom?:string|null; AvailableDateTo?:string|null; AvailableDates:string[]; TourTypeIds:number[]; DescriptionUrl?:string; DescriptionHtml?:string; BookingConditionsHtml?:string; Status?:number }
export interface TourServiceDto { Id:number; TourId:number; ServiceTypeId:number; Name:string; OrderIndex:number; ParentServiceId?:number; IsHidden:boolean; IsRemovable:boolean; IsOptional:boolean; Revision:number }
export interface CreateTourServiceCommand { ServiceTypeId:number; Name:string; ParentServiceId?:number|null; IsHidden:boolean; IsRemovable:boolean; IsOptional:boolean }
export interface UpdateTourServiceCommand { Name:string; ParentServiceId?:number|null; IsHidden:boolean; IsRemovable:boolean; IsOptional:boolean }
export interface TourHotelDto { Id?:number; OrderIndex:number; CountryId:number; CityIds:number[]; DurationsInNights:number[]; HotelIds:number[] }
export interface TourHotelGroupDto { Id?:number; Name:string; Priority:number; OrderIndex:number; Hotels:TourHotelDto[] }
export interface TourFlightDto { Id?:number; OrderIndex:number; DepartureCountryIds:number[]; DepartureCityIds:number[]; ArrivalCountryIds:number[]; ArrivalCityIds:number[]; AirlineIds:number[]; FlightIds:number[]; DepartureAirportIds:number[]; ArrivalAirportIds:number[]; FareIds:number[]; MinConnectionMinutes?:number|null; MaxConnectionMinutes?:number|null }
export interface TourFlightGroupDto { Id?:number; Name:string; Priority:number; OrderIndex:number; Flights:TourFlightDto[] }
export interface TourBusTransferDto { Id?:number; DepartureCountryIds:number[]; DepartureCityIds:number[]; ArrivalCountryIds:number[]; ArrivalCityIds:number[]; BusTransferIds:number[]; BoardingPointIds:number[]; TransportIds:number[]; DepartureTimeFrom:string; DepartureTimeTo:string; ArrivalTimeFrom:string; ArrivalTimeTo:string }
export interface TourRailwayTransferDto { Id?:number; DepartureCountryIds:number[]; DepartureCityIds:number[]; ArrivalCountryIds:number[]; ArrivalCityIds:number[]; RailwayTransferIds:number[]; TransportIds:number[]; DepartureTimeFrom:string; DepartureTimeTo:string; ArrivalTimeFrom:string; ArrivalTimeTo:string }
export interface LookupItem { Id:number; Name:string; CountryId?:number; CityId?:number; Code?:string }
