import React, { useEffect, useState } from "react";
import { View } from "react-native";
import { GooglePlacesAutocomplete } from "react-native-google-places-autocomplete";
import MapView, { Marker } from "react-native-maps";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { GOOGLE_MAPS_API_KEY } from "../constants";

const MapScreen = () => {
    const [location, setLocation] = useState({latitude: 10.8700233, longtitude: 106.8025735});

    const handleSetLocation = (lat, lng) => {
        setLocation({
            latitude: lat,
            longtitude: lng,
        });
    }

    return (
        <View className="flex-1">
            {/* <Text className='text-xl font-bold text-center p-2 bg-white'>Map</Text> */}
            <GooglePlacesAutocomplete
                placeholder="Nhập địa chỉ của bạn"
                debounce={5000}
                onPress={(data, details = null) => {
                    handleSetLocation(details.geometry.location.lat, details.geometry.location.lng)
                }}
                query={{
                    key: GOOGLE_MAPS_API_KEY,
                    language: "en",
                }}
                keyboardShouldPersistTaps={"always"}
                fetchDetails={true}
                enablePoweredByContainer={false}
                style={{ zIndex: 999, position: "absolute", top: wp(9) }}
            />

            {location && (
                <MapView
                    provider="google"
                    initialRegion={{
                        latitude: 10.8700233,
                        longtitude: 106.8025735,
                        latitudeDelta: 0.9,
                        longtitudeDelta: 0.9,
                    }}
                    style={{ width: wp(100), height: hp(88), zIndex: -10 }}
                    region={{
                        longitude: location?.longtitude,
                        latitude: location?.latitude,
                        latitudeDelta: 0.01,
                        longitudeDelta: 0.01,
                    }}
                >
                    <Marker
                        coordinate={{
                            longitude: location?.longtitude,
                            latitude: location?.latitude,
                        }}
                    />
                </MapView>
            )}
        </View>
    );
};

export default MapScreen;
