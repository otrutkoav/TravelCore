export interface NavigationItem { label:string; to?:string; children?:NavigationItem[] }
export const navigation:NavigationItem[]=[
 {label:'Главная',to:'/'},
 {label:'Туры',children:[{label:'Список туров',to:'/tours'},{label:'Типы туров',to:'/catalog/tour-types'}]},
 {label:'География',children:[{label:'Страны',to:'/catalog/countries'},{label:'Регионы',to:'/catalog/regions'},{label:'Города',to:'/catalog/cities'},{label:'Курорты',to:'/catalog/resorts'}]},
 {label:'Отели',children:[{label:'Отели',to:'/catalog/hotels'},{label:'Категории',to:'/catalog/hotel-categories'},{label:'Типы комнат',to:'/catalog/room-types'},{label:'Категории комнат',to:'/catalog/room-categories'},{label:'Типы питания',to:'/catalog/meal-types'},{label:'Типы размещения',to:'/catalog/accommodation-types'},{label:'Правила размещения',to:'/catalog/accommodation-rules'}]},
 {label:'Авиа',children:[{label:'Авиакомпании',to:'/catalog/airlines'},{label:'Воздушные суда',to:'/catalog/aircraft'},{label:'Аэропорты',to:'/catalog/airports'},{label:'Классы',to:'/catalog/air-classes'},{label:'Чартеры',to:'/catalog/charters'},{label:'Сезоны чартеров',to:'/catalog/charter-seasons'}]},
 {label:'Транспорт и услуги',children:[{label:'Виды услуг',to:'/catalog/service-types'},{label:'Трансферы',to:'/catalog/transfers'},{label:'Направления трансферов',to:'/catalog/transfer-directions'},{label:'Транспорт',to:'/catalog/transports'},{label:'Автобусные трансферы',to:'/catalog/bus-transfers'},{label:'Точки автобусных трансферов',to:'/catalog/bus-transfer-points'},{label:'Автобусные расписания',to:'/catalog/bus-schedules'},{label:'Ж/д трансферы',to:'/catalog/railway-transfers'},{label:'Расписания поездов',to:'/catalog/train-schedules'}]},
 {label:'Схемы мест',children:[{label:'Планы транспорта',to:'/catalog/vehicle-plans'},{label:'Места',to:'/catalog/seating-cells'}]},
 {label:'Финансы',children:[{label:'Курсы',to:'/catalog/rates'},{label:'Реальные курсы',to:'/catalog/real-courses'}]},
]
