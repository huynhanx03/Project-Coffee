import { View, Text } from 'react-native'
import React, { useCallback, useEffect, useState } from 'react'
import { getAddress } from '../controller/AddressController'
import { useFocusEffect } from '@react-navigation/native'
import db from "../firebase";


const getDefaultAddress = () => {
    const [addressData, setAddressData] = useState(null)

    const handleGetAddresses = async () => {
        try {
            const addresses = await getAddress();
            if (addresses) {
                let addressDefault = null;
                for (const address in addresses) {
                    if (addresses[address].Default) {
                        addressDefault = addresses[address];
                        break;
                    }
                }
                if (addressDefault) {
                    setAddressData(addressDefault);
                }
            }
        } catch (err) {
            console.log(err);
        }
    }

    useFocusEffect(
        useCallback(() => {
            handleGetAddresses();
        }, [])
    )

    useEffect(() => {
        handleGetAddresses();
    }, [])

    return addressData
}

export default getDefaultAddress