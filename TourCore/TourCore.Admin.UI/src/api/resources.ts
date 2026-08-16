import { http } from './http'
import type { CreateTourServiceCommand,LookupItem,PagedResponse,TourBusTransferDto,TourCommand,TourDto,TourFlightGroupDto,TourHotelGroupDto,TourListItemDto,TourRailwayTransferDto,TourServiceDto,UpdateTourServiceCommand } from './contracts'

export const toursApi={
  async list(params:Record<string,unknown>){return (await http.get<PagedResponse<TourListItemDto>>('/api/v1/tours',{params})).data},
  async get(id:number){return (await http.get<TourDto>(`/api/v1/tours/${id}`)).data},
  async create(command:TourCommand){return (await http.post<TourDto>('/api/v1/tours',command)).data},
  async update(id:number,command:TourCommand){return (await http.put<TourDto>(`/api/v1/tours/${id}`,command)).data},
}
export const tourServicesApi={
  async list(tourId:number){return (await http.get<TourServiceDto[]>(`/api/v1/tours/${tourId}/services`)).data},
  async create(tourId:number,command:CreateTourServiceCommand){return (await http.post<TourServiceDto>(`/api/v1/tours/${tourId}/services`,command)).data},
  async update(tourId:number,id:number,command:UpdateTourServiceCommand){return (await http.put<TourServiceDto>(`/api/v1/tours/${tourId}/services/${id}`,command)).data},
  async remove(tourId:number,id:number){await http.delete(`/api/v1/tours/${tourId}/services/${id}`)},
  async reorder(tourId:number,serviceIds:number[]){return (await http.put<TourServiceDto[]>(`/api/v1/tours/${tourId}/services/order`,{ServiceIds:serviceIds})).data},
}
export const tourHotelsApi={
  async get(tourId:number,serviceId:number){return (await http.get<TourHotelGroupDto[]>(`/api/v1/tours/${tourId}/services/${serviceId}/hotels`)).data},
  async replace(tourId:number,serviceId:number,groups:TourHotelGroupDto[]){return (await http.put<TourHotelGroupDto[]>(`/api/v1/tours/${tourId}/services/${serviceId}/hotels`,{Groups:groups})).data},
}
export const tourFlightsApi={
  async get(tourId:number,serviceId:number){return (await http.get<TourFlightGroupDto[]>(`/api/v1/tours/${tourId}/services/${serviceId}/flights`)).data},
  async replace(tourId:number,serviceId:number,groups:TourFlightGroupDto[]){return (await http.put<TourFlightGroupDto[]>(`/api/v1/tours/${tourId}/services/${serviceId}/flights`,{Groups:groups})).data},
}
export const tourBusTransferApi={
  async get(tourId:number,serviceId:number){return (await http.get<TourBusTransferDto>(`/api/v1/tours/${tourId}/services/${serviceId}/bus-transfer`)).data},
  async replace(tourId:number,serviceId:number,command:TourBusTransferDto){return (await http.put<TourBusTransferDto>(`/api/v1/tours/${tourId}/services/${serviceId}/bus-transfer`,command)).data},
}
export const tourRailwayTransferApi={
  async get(tourId:number,serviceId:number){return (await http.get<TourRailwayTransferDto>(`/api/v1/tours/${tourId}/services/${serviceId}/railway-transfer`)).data},
  async replace(tourId:number,serviceId:number,command:TourRailwayTransferDto){return (await http.put<TourRailwayTransferDto>(`/api/v1/tours/${tourId}/services/${serviceId}/railway-transfer`,command)).data},
}
export async function loadLookup(path:string,params:Record<string,unknown>={}){return (await http.get<PagedResponse<LookupItem>>(path,{params:{pageSize:100,...params}})).data.Items}
