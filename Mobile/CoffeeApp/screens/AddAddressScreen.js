import React, { useEffect, useState } from "react";
import { Pressable, Text, TextInput, TouchableOpacity, View } from "react-native";
import { GooglePlacesAutocomplete } from "react-native-google-places-autocomplete";
import * as Icons from "react-native-heroicons/outline";
import { Divider } from "react-native-paper";
import { SafeAreaView } from "react-native-safe-area-context";
import { colors } from "../theme";
import { useNavigation } from "@react-navigation/native";
import { addAddress } from "../controller/AddressController";
import Toast from "react-native-toast-message";
import { GOOGLE_MAPS_API_KEY } from "../constants";
import Header from "../components/header";

const AddAddressScreen = () => {
    const navigation = useNavigation();
    const [location, setLocation] = useState(null);
    const [name, setName] = useState("");
    const [phone, setPhone] = useState("");
    const [street, setStreet] = useState("");

    const handleAddAddress = async () => {
        try {
            const result = await addAddress(name, phone, street, location);

            Toast.show({
                type: result[0] ? "success" : "error",
                text1: 'Thông báo',
                text2: result[1],
                topOffset: 70,
                text1Style: {fontSize: 18},
                text2Style: {fontSize: 15},
                onHide: () => result[0] ? navigation.goBack() : null,
                onPress: () => result[0] ? navigation.goBack() : null
            })
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <View className="flex-1">
            <Header title={'Địa chỉ mới'} />

            <View className="space-y-2 mt-5">
                <Text className="text-gray-600 px-2">Liên hệ</Text>
                <View>
                    <TextInput
                        placeholder="Họ và tên"
                        className="bg-white p-2 text-base"
                        value={name}
                        onChangeText={(e) => setName(e)}
                    />
                    <Divider />
                    <TextInput
                        placeholder="Số điện thoại"
                        className="bg-white p-2 text-base"
                        value={phone}
                        onChangeText={(e) => setPhone(e)}
                    />
                </View>
            </View>

            <View className="mt-5 mb-2">
                <Text className="text-gray-600 px-2">Địa chỉ</Text>
            </View>

            <TextInput
                placeholder="Số nhà, tên đường"
                className="bg-white p-2 text-base"
                value={street}
                onChangeText={(e) => setStreet(e)}
            />
            <Divider />
            <GooglePlacesAutocomplete
                placeholder="Nhập địa chỉ của bạn"
                styles={{
                    textInput: {
                        lineHeight: 24,
                        fontSize: 16,
                    },
                }}
                debounce={500}
                onPress={(data, details = null) => {
                    setLocation({
                        latitude: details.geometry.location.lat,
                        longtitude: details.geometry.location.lng,
                        address: details.formatted_address,
                    });
                }}
                query={{
                    key: GOOGLE_MAPS_API_KEY,
                    language: "en",
                }}
                fetchDetails={true}
                enablePoweredByContainer={false}
            />

            <View className="mb-5 mx-5 rounded-md">
                <TouchableOpacity onPress={handleAddAddress} className="p-3 rounded-md bg-amber-500">
                    <Text className="text-lg font-semibold text-center">Thêm địa chỉ</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default AddAddressScreen;
