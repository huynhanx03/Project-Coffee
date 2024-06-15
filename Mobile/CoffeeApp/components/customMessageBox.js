import { View, Text, Pressable, Modal, TouchableOpacity, Image } from 'react-native';
import React, { useEffect } from 'react';
import { useNotification } from '../context/ModalContext';
import { heightPercentageToDP as hp, widthPercentageToDP as wp } from 'react-native-responsive-screen';
import { colors } from '../theme';
import { MESSAGE_TYPE } from '../constants';

const CustomMessageBox = () => {
    const { isVisible, message, type, hideNotification, onClick } = useNotification();
    const [title, setTitle] = React.useState('');
    useEffect(() => {
        if (type === MESSAGE_TYPE.SUCCESS) {
            setTitle('Thành công');
        } else if (type === MESSAGE_TYPE.ERROR) {
            setTitle('Lỗi');
        } else {
            setTitle('Thông báo');
        }
    }, [])

    const handleOnClick = () => {
        if (onClick) {
            onClick();
        }
        hideNotification();
    }
    return (
        <Modal
            visible={isVisible}
            transparent={true}
            animationType="fade"
            onDismiss={hideNotification}>
            <View
                className="flex-1 justify-center items-center"
                style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                <View
                    className="bg-white items-center justify-center rounded-[25px] shadow-xl"
                    style={{ width: wp(85), height: hp(25) }}>
                    <View className="space-y-2 items-center">
                        <Text
                            className="font-bold text-2xl"
                            style={{ color: colors.text(1) }}>
                            {title}
                        </Text>
                        <Text className="text-lg text-center">{message}</Text>
                    </View>

                    {
                        type === MESSAGE_TYPE.INFORM ? (
                            <View className='flex-row absolute bottom-4 items-center w-[85%] justify-between'>
                                <TouchableOpacity onPress={hideNotification} className='rounded-3xl items-center shadow-md' style={{width: wp(35), backgroundColor: colors.primary}}>
                                    <View className="p-2">
                                        <Text className="text-xl font-bold text-white">Đóng</Text>
                                    </View>
                                </TouchableOpacity>
                                <TouchableOpacity onPress={handleOnClick} className='rounded-3xl items-center shadow-md' style={{width: wp(35), backgroundColor: colors.active}}>
                                    <View className="p-2">
                                        <Text className="text-xl font-bold text-white">Đồng ý</Text>
                                    </View>
                                </TouchableOpacity>
                            </View>
                        ) : (
                            <TouchableOpacity onPress={hideNotification} className='absolute bottom-4 rounded-3xl items-center' style={{width: wp(70), backgroundColor: colors.primary}}>
                                <View className="p-2">
                                    <Text className="text-xl font-bold text-white">Đóng</Text>
                                </View>
                            </TouchableOpacity>
                        )
                    }

                    <View
                        className="absolute"
                        style={{ top: hp(-6) }}>
                            {
                                type === MESSAGE_TYPE.SUCCESS ? (
                                    <Image source={require('../assets/icons/SuccessMessage.png')} />
                                ) : type === MESSAGE_TYPE.ERROR ? (
                                    <Image source={require('../assets/icons/ErrorMessage.png')} />
                                ) : (
                                    <Image source={require('../assets/icons/InformMessage.png')} />
                                )
                            }
                    </View>
                </View>
            </View>
        </Modal>
    );
};

export default CustomMessageBox;
