import { View, Text, TouchableOpacity, Pressable, ScrollView } from "react-native";
import React, { useEffect, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Icons from "react-native-heroicons/outline";
import { widthPercentageToDP as wp } from "react-native-responsive-screen";
import { colors } from "../theme";
import Address from "../components/address";
import { useNavigation } from "@react-navigation/native";
import { getAddress } from "../controller/AddressController";

const AddressScreen = () => {
    const navigation = useNavigation();
    const [addressData, setAddressData] = useState(null)

    const handleGetAddresses = async () => {
        try {
            const addresses = await getAddress();
            if (addresses) {
                setAddressData(addresses);
            }
        } catch (err) {
            console.log(err);
        }
    }

    useEffect(() => {
        handleGetAddresses();
    }, [])

    return (
        <View className='flex-1'>
            <SafeAreaView
                style={{
                    backgroundColor: "#f2f2f2",
                    shadowColor: "#000000",
                    shadowOffset: { width: 0, height: 4 },
                    shadowOpacity: 0.19,
                    shadowRadius: 5.62,
                    elevation: 6,
                }}>
                {/* header */}
                <View className="flex-row justify-between items-center mx-5">
                    <TouchableOpacity onPress={() => navigation.goBack()}>
                        <Icons.ChevronLeftIcon size={30} color={colors.active} />
                    </TouchableOpacity>
                    <Text className="text-xl font-semibold tracking-wider">Địa chỉ</Text>
                    <TouchableOpacity onPress={() => navigation.navigate('AddAddress')}>
                        <Icons.PlusIcon size={30} color={colors.active} />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>

            <ScrollView showsVerticalScrollIndicator={false} className='pt-2 mx-4'>
                {addressData && <Address data={addressData} />}
            </ScrollView>
        </View>
    );
};

export default AddressScreen;
