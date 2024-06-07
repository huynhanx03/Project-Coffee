import { View, Text, Modal, ActivityIndicator } from 'react-native';
import React from 'react';
import { colors } from '../theme';
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import * as Progress from 'react-native-progress';

const ModalLoading = ({ isLoading }) => {
    return (
        <Modal
            transparent={true}
            animationType="fade"
            visible={isLoading}>
            <View
                className="flex-1 justify-center items-center"
                style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                <View>
                    <Progress.CircleSnail size={150} color={'#ba7b52'} thickness={10} indeterminate={false} />
                </View>
            </View>
        </Modal>
    );
};

export default ModalLoading;
