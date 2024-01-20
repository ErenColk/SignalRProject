using AutoMapper;
using SignalR.DtoLayer.BookingDto;
using SignalR.DtoLayer.ContactDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
	public class BookingMapping : Profile
	{

		public BookingMapping()
		{
			CreateMap<Booking, ResultBookingDto>();
			CreateMap<Booking, CreateBookingDto>();
			CreateMap<Booking, UpdateBookingDto>();
			CreateMap<Booking, GetContactDto>();
		}
	}
}
