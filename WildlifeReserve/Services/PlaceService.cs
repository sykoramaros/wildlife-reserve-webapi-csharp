using WildlifeReserve.Enums;
using WildlifeReserve.EnumsDetails;

namespace WildlifeReserve.Services;

public class PlaceService {
    public static PlaceDetails GetPlaceDetails(Place place) {
        switch (place) {
            case Place.Ostrava:
                return new PlaceDetails() {
                    Lat = 49.7,
                    Lng = 18.1,
                    Radius = 10
                };
            case Place.Ulaanbaatar:
                return new PlaceDetails() {
                    Lat = 47.9,
                    Lng = 106.9,
                    Radius = 10
                };
            default:
                return new PlaceDetails() {
                    Lat = 0,
                    Lng = 0,
                    Radius = 0
                };
        }
    }
}