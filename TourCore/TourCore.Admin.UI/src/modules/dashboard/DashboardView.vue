<template>
 <section>
  <div class="page-heading"><div><p class="eyebrow">TourCore Admin</p><h2>Управление туристическим продуктом</h2><p>Интерфейс управления TourCore на PostgreSQL.</p></div></div>
  <div class="dashboard-grid">
   <RouterLink to="/tours" class="feature-card"><span>Туры</span><strong>Шаблоны и состав туров</strong><p>Даты, услуги, перелёты и проживание.</p></RouterLink>
   <RouterLink to="/catalog/hotels" class="feature-card"><span>Отели</span><strong>Гостиничный каталог</strong><p>Отели и справочники размещения.</p></RouterLink>
   <RouterLink to="/catalog/cities" class="feature-card"><span>География</span><strong>Страны и города</strong><p>Географическая структура продукта.</p></RouterLink>
   <RouterLink to="/catalog/charters" class="feature-card"><span>Авиа</span><strong>Чартерная программа</strong><p>Рейсы, сезоны и авиационные справочники.</p></RouterLink>
  </div>
 </section>
</template>
