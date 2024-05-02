import { View, Text, TouchableOpacity, ScrollView } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Icons from "react-native-heroicons/solid";
import ItemOrder from "../components/itemOrder";
import { useNavigation } from "@react-navigation/native";
import { StatusBar } from "expo-status-bar";

const OrderInfoScreen = () => {
    const navigation = useNavigation()
    return (
        <View className="flex-1">
            <StatusBar style="dark" />
            <SafeAreaView
                style={{
                    backgroundColor: "#f2f2f2",
                    shadowColor: "#000000",
                    shadowOffset: { width: 0, height: 4 },
                    shadowOpacity: 0.19,
                    shadowRadius: 5.62,
                    elevation: 6,
                }}
            >
                {/* header */}

                <View className="flex-row justify-between items-center mx-5">
                    <TouchableOpacity onPress={() => navigation.goBack()}>
                        <Icons.ChevronLeftIcon size={30} color={"black"} />
                    </TouchableOpacity>
                    <Text className="text-lg font-semibold">
                        Thông tin đơn hàng
                    </Text>

                    <TouchableOpacity>
                        <Icons.HeartIcon
                            size={30}
                            color={"transparent"}
                        />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>

            <ScrollView showsVerticalScrollIndicator={false} className='pt-1' contentContainerStyle={{padding: 8}}>
                <ItemOrder />
                <ItemOrder />
                <ItemOrder />
            </ScrollView>
        </View>
    );
};

export default OrderInfoScreen;
