import { View, Text } from "react-native";
import React from "react";
import ItemVoucher from "./itemVoucher";
import Animated, { FadeInUp, FadeOut } from "react-native-reanimated";

const ItemVoucherList = (props) => {
    const voucherList = props.voucherList;
    return (
        <Animated.View entering={FadeInUp.duration(1000)} exiting={FadeOut} className='flex-1'>
            {
                voucherList && voucherList.map((voucher) => {
                    return (
                        <ItemVoucher key={voucher.MaPhieuGiamGia} voucher={voucher}/>
                    )
                })
            }
        </Animated.View>
    );
};

export default ItemVoucherList;
