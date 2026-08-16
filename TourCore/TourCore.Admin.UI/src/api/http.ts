import axios, { AxiosError } from 'axios'
import type { ApiErrorResponse } from './contracts'

export class AppError extends Error {
  constructor(message:string, public readonly code='unknown_error', public readonly status?:number, public readonly fieldErrors:Record<string,string[]>={}) { super(message) }
}
const config = window.__TOURCORE_CONFIG__
export const http = axios.create({ baseURL:config?.apiBaseUrl?.replace(/\/$/,'')??'', timeout:30000, headers:{Accept:'application/json'} })
http.interceptors.response.use(undefined,(error:AxiosError<ApiErrorResponse>)=>{
  if(!error.response) return Promise.reject(new AppError('Не удалось подключиться к API.','network_error'))
  const body=error.response.data
  const fieldErrors:Record<string,string[]>={}
  if(body?.Details) for(const [field,errors] of Object.entries(body.Details)) fieldErrors[field]=errors.map(x=>x.Message)
  return Promise.reject(new AppError(body?.Message||'API вернул ошибку.',body?.Code||'http_error',error.response.status,fieldErrors))
})
