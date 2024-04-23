import { View, Text, Pressable } from "react-native";
import React, { useState, useEffect } from "react";
import MapView, { Marker } from "react-native-maps";
import {
    widthPercentageToDP as wp,
    heightPercentageToDP as hp,
} from "react-native-responsive-screen";
import { GooglePlacesAutocomplete } from "react-native-google-places-autocomplete";
import axios from "axios";
import { GOOGLE_MAPS_API_KEY } from "../constants";

const MapScreen = () => {
    const [location, setLocation] = useState(null);

    useEffect(() => {
        setLocation({
            latitude: 10.8700233,
            longtitude: 106.8025735,
        });
    }, []);

    return (
        <View className="flex-1">
            {/* <Text className='text-xl font-bold text-center p-2 bg-white'>Map</Text> */}
            <GooglePlacesAutocomplete
                placeholder="Nhập địa chỉ của bạn"
                debounce={500}
                onPress={(data, details = null) => {
                    setLocation({
                        latitude: details.geometry.location.lat,
                        longtitude: details.geometry.location.lng,
                    });
                }}
                query={{
                    key: GOOGLE_MAPS_API_KEY,
                    language: "en",
                }}
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
