using TourCore.Application.RealCourses.DTOs;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.RealCourses.Mappings
{
    public static class RealCourseMappingExtensions
    {
        public static RealCourseDto ToDto(this RealCourse entity)
        {
            return new RealCourseDto
            {
                Id = entity.Id,
                FromRateCode = entity.FromRateCode,
                ToRateCode = entity.ToRateCode,
                Course = entity.Course,
                CentralBankCourse = entity.CentralBankCourse,
                DateBeg = entity.DateBeg,
                DateEnd = entity.DateEnd,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static RealCourseListItemDto ToListItemDto(this RealCourse entity)
        {
            return new RealCourseListItemDto
            {
                Id = entity.Id,
                FromRateCode = entity.FromRateCode,
                ToRateCode = entity.ToRateCode,
                Course = entity.Course,
                CentralBankCourse = entity.CentralBankCourse,
                DateBeg = entity.DateBeg,
                DateEnd = entity.DateEnd
            };
        }
    }
}