import { View, Text, TouchableOpacity, ScrollView } from "react-native";
import React, { useEffect, useState } from "react";
import { getVoucher } from "../controller/VoucherController";
import ItemVoucherList from "../components/itemVoucherList";
import { SafeAreaView } from "react-native-safe-area-context";
import { useNavigation } from "@react-navigation/native";
import * as Icons from "react-native-heroicons/outline";

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
                        Phiếu giảm giá
                    </Text>

                    <TouchableOpacity>
                        <Icons.HeartIcon
                            size={30}
                            color={"transparent"}
                        />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>
            
            <ScrollView className='flex-1'>
                {voucherList && <ItemVoucherList voucherList={voucherList}/>}
            </ScrollView>
        </View>
    );
};

export default VoucherScreen;
