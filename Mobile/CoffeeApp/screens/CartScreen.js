import { useFocusEffect, useNavigation } from "@react-navigation/native";
import React, { useCallback, useEffect, useState } from "react";
import {
    Pressable,
    ScrollView,
    Text,
    TouchableOpacity,
    View,
} from "react-native";
import * as Icons from "react-native-heroicons/outline";
import { Divider } from "react-native-paper";
import { widthPercentageToDP as wp } from "react-native-responsive-screen";
import { SafeAreaView } from "react-native-safe-area-context";
import ItemCart from "../components/itemCart";
import { getAddress } from "../controller/AddressController";
import getDefaultAddress from "../customHooks/getDefaultAddress";

const CartScreen = () => {
    const navigation = useNavigation();

    const addressData = getDefaultAddress();

    return (
        <View className="flex-1">
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
                        <Icons.ChevronLeftIcon size={30} color={"black"} />
                    </TouchableOpacity>
                    <Text className="text-lg font-semibold tracking-wider">
                        Giỏ hàng
                    </Text>
                    <TouchableOpacity>
                        <Icons.TrashIcon size={30} color={"black"} />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>

            <ScrollView
                className="mx-5 pt-1 space-y-3"
                showsVerticalScrollIndicator={false}>
                {/* address */}
                <View className="flex-row items-center gap-3">
                    <Icons.MapPinIcon size={24} color={"red"} />
                    <Text className="text-base">Địa chỉ nhận hàng</Text>
                </View>

                <Pressable
                    onPress={() => navigation.navigate("Address")}
                    className="ml-9 space-y-1 flex-row justify-between">
                    <View style={{ width: wp(70) }}>
                        <View className="flex-row gap-1 items-center">
                            <Text className="text-base">
                                {addressData?.HoTen}
                            </Text>
                            <Text>|</Text>
                            <Text className="text-base">
                                {addressData?.SoDienThoai}
                            </Text>
                        </View>
                        <View>
                            <Text>{addressData?.DiaChi}</Text>
                        </View>
                    </View>

                    <View>
                        <Icons.ChevronRightIcon size={24} color={"black"} />
                    </View>
                </Pressable>

                <Divider />

                {/* item cart */}
                <View className="space-y-2">
                    <ItemCart />
                    <ItemCart />
                    <ItemCart />
                    <ItemCart />
                </View>
            </ScrollView>

            <View className="absolute bottom-5 w-full">
                <TouchableOpacity className="flex-row justify-between items-center mx-5 bg-amber-500 rounded-full p-5">
                    <Text className="text-lg font-semibold">Thanh toán</Text>
                    <Text className="text-lg font-semibold">100.000đ</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default CartScreen;
