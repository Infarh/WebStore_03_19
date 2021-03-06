﻿using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Map
{
    public static class SectionsMapper
    {
        public static void CopyTo(this Section section, SectionViewModel model)
        {
            model.Id = section.Id;
            model.Name = section.Name;
            model.Order = section.Order;
        }

        public static SectionViewModel CreateViewModel(this Section section)
        {
            var model = new SectionViewModel();
            section.CopyTo(model);
            return model;
        }

        public static void CopyTo(this SectionViewModel model, Section section)
        {
            section.Name = model.Name;
            section.Order = model.Order;
        }

        public static Section Create(this SectionViewModel model)
        {
            var section = new Section();
            model.CopyTo(section);
            return section;
        }

        public static SectionDTO ToDTO(this Section section) => section is null ? null : new SectionDTO
        {
            Id = section.Id,
            Name = section.Name
        };

        public static Section FromDTO(this SectionDTO section) => section is null ? null : new Section
        {
            Id = section.Id,
            Name = section.Name
        };
    }
}
