import { View, Text, TouchableOpacity, ScrollView } from "react-native";
import React, { useEffect, useState } from "react";
import { getVoucher } from "../controller/VoucherController";
import ItemVoucherList from "../components/itemVoucherList";
import { SafeAreaView } from "react-native-safe-area-context";
import { useNavigation } from "@react-navigation/native";
import * as Icons from "react-native-heroicons/outline";
import Header from "../components/header";

const VoucherScreen = () => {
    const navigation = useNavigation()
    const [voucherList, setVoucherList] = useState([])
    const handleGetVoucher = async () => {
        try {
            const vouchers = await getVoucher();
            setVoucherList(vouchers)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        handleGetVoucher()
    }, [])
    return (
        <View className='flex-1'>
            <Header title={'Phiếu giảm giá'}/>
            
            <ScrollView className='flex-1'>
                {voucherList && <ItemVoucherList voucherList={voucherList}/>}
            </ScrollView>
        </View>
    );
};

export default VoucherScreen;
