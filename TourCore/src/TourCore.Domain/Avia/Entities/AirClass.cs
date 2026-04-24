using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Avia.Entities
{
    /// <summary>
    /// Класс обслуживания в авиа (эконом, бизнес и т.д.).
    /// </summary>
    public class AirClass : AuditableEntity
    {
        /// <summary>
        /// Код класса обслуживания.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Название класса обслуживания.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Альтернативное / латинское название.
        /// </summary>
        public string NameEn { get; protected set; }

        /// <summary>
        /// Группа класса (если используется).
        /// </summary>
        public string Group { get; protected set; }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        public int SortOrder { get; protected set; }

        protected AirClass()
        {
        }

        public AirClass(
            string code,
            string name,
            DateTime createdAt,
            string nameEn = null,
            string group = null,
            int sortOrder = 0)
        {
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);
            SetGroup(group);
            SetSortOrder(sortOrder);

            SetCreated(createdAt);
        }

        public void Update(
            string code,
            string name,
            DateTime updatedAt,
            string nameEn = null,
            string group = null,
            int sortOrder = 0)
        {
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);
            SetGroup(group);
            SetSortOrder(sortOrder);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Air class code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 3)
                throw new DomainException("Air class code must be 3 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Air class name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Air class name must be 100 characters or less.");

            Name = name;
        }

        private void SetNameEn(string nameEn)
        {
            if (string.IsNullOrWhiteSpace(nameEn))
            {
                NameEn = null;
                return;
            }

            nameEn = nameEn.Trim();

            if (nameEn.Length > 100)
                throw new DomainException("Air class alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetGroup(string group)
        {
            if (string.IsNullOrWhiteSpace(group))
            {
                Group = null;
                return;
            }

            group = group.Trim();

            if (group.Length > 128)
                throw new DomainException("Air class group must be 128 characters or less.");

            Group = group;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Air class sort order cannot be negative.");

            SortOrder = sortOrder;
        }
    }
}