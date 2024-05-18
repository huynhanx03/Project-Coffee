import { View, Text, TouchableOpacity, ScrollView } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Icons from "react-native-heroicons/solid";
import ItemOrder from "../components/itemOrder";
import { useNavigation } from "@react-navigation/native";
import { StatusBar } from "expo-status-bar";
import ItemOrderList from "../components/itemOrderList";
import Header from "../components/header";

const OrderInfoScreen = () => {
    const navigation = useNavigation()
    return (
        <View className="flex-1">
            <StatusBar style="dark" />
            <Header title='Thông tin đơn hàng'/>

            <ScrollView showsVerticalScrollIndicator={false} contentContainerStyle={{padding: 8}}>
                <ItemOrderList />
            </ScrollView>
        </View>
    );
};

export default OrderInfoScreen;
