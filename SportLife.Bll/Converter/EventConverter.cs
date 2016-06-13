using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using SportLife.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Converter
{
    public static class EventConverter
    {
        public static EventViewModel FromDbModelToViewModel(Event eventDb)
        {
            EventViewModel model = null;
            if (eventDb != null)
            {
                model = new EventViewModel
               {
                   CreatorId = eventDb.CreatorId,
                   EventDate = eventDb.EventDate,
                   EventDescription = eventDb.EventDescription,
                   EventFee = eventDb.EventFee,
                   EventName = eventDb.EventName,
                   EventPlace = eventDb.EventPlace,
                   EventPoster = eventDb.EventPoster,
                   ClosingRegistrationDate = eventDb.ClosingRegistrationDate,
                   DateCreation = eventDb.DateCreation,
                   IsOpened = eventDb.IsOpened,
                   IsVisible = eventDb.IsVisible,
                   MaxNumberOfParticipants = eventDb.MaxNumberOfParticipants,
                   EventId = eventDb.EventId,
                   Type = EventConverter.FromDbTypeRoEnum(eventDb.Type)
               };
            }

            return model;
        }

        public static Event FromViewModelToDbModel(EventViewModel model)
        {
            Event eventModel = null;
            if (model != null)
            {
                eventModel = new Event
                {
                    EventId = model.EventId,
                    CreatorId = model.CreatorId,
                    EventDate = model.EventDate.HasValue ? model.EventDate.Value : DateTime.Now,
                    EventDescription = model.EventDescription,
                    EventFee = model.EventFee,
                    EventName = model.EventName,
                    EventPlace = model.EventPlace,
                    EventPoster = model.EventPoster,
                    ClosingRegistrationDate = model.ClosingRegistrationDate,
                    DateCreation = model.DateCreation,
                    IsOpened = model.IsOpened,
                    IsVisible = model.IsVisible,
                    MaxNumberOfParticipants = model.MaxNumberOfParticipants,
                    Type = (int)model.Type
                };
            }

            return eventModel;
        }

        public static EventTypeEnum FromDbTypeRoEnum(int type)
        {
            return ((EventTypeEnum[])Enum.GetValues(typeof(EventTypeEnum))).FirstOrDefault(x => (int)x == type);
        }
    }
}
