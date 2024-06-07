import React, { useEffect, useState } from "react";
import { Pressable, Text, TouchableOpacity, View } from "react-native";
import * as Icons from "react-native-heroicons/outline";
import { widthPercentageToDP as wp } from "react-native-responsive-screen";
import { setDefaultAddress } from "../controller/AddressController";

const Address = ({ data }) => {
    addressKeys = Object.keys(data);
    const [defaultAddressUI, setDefaultAddressUI] = useState("");
    let defaultAddress = "";

    for (let i = 0; i < addressKeys.length; i++) {
        if (data[addressKeys[i]].Default) {
            defaultAddress = addressKeys[i];
            break;
        }
    }

    useEffect(() => {
        setDefaultAddressUI(defaultAddress);
    }, [defaultAddress]);

    const handleDefaultAddress = async (addressKey) => {
        setDefaultAddressUI(addressKey);
        await setDefaultAddress(addressKey);
    };

    return (
        <View>
            {addressKeys.map((addressKey, index) => {
                let checked = defaultAddressUI == addressKey ? "bg-amber-400" : "bg-white";
                return (
                    <View key={index} className="mt-2 border border-gray-300 rounded-xl bg-white">
                        <Pressable
                            onPress={() => handleDefaultAddress(addressKey)}
                            className="p-2 space-y-1 flex-row justify-between items-center">
                            <TouchableOpacity onPress={() => handleDefaultAddress(addressKey)}>
                                <View className={"border-2 border-gray-500 p-3 rounded-full " + checked} />
                            </TouchableOpacity>

                            <View style={{ width: wp(70) }} className="space-y-1">
                                <View className="flex-row gap-1 items-center">
                                    <Text className="font-semibold text-base">{data[addressKey]?.HoTen}</Text>
                                    <Text className="text-base">|</Text>
                                    <Text className="font-semibold text-base">{data[addressKey]?.SoDienThoai}</Text>
                                </View>
                                <View>
                                    <Text>{data[addressKey]?.DiaChi}</Text>
                                </View>
                            </View>
                            <View>
                                <Icons.ChevronRightIcon size={24} color={"black"} />
                            </View>
                        </Pressable>
                    </View>
                );
            })}
        </View>
    );
};

export default Address;
