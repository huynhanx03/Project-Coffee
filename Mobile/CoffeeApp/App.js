import 'react-native-gesture-handler'
import { View, Text } from 'react-native'
import React from 'react'
import { Modal, PaperProvider } from 'react-native-paper'
import Navigation from './navigation/Navigation'
import { Provider } from 'react-redux'
import store from './redux/store'
import Toast from 'react-native-toast-message'
import { GestureHandlerRootView } from 'react-native-gesture-handler'
import { NotificationProvider } from './context/ModalContext'
import CustomMessageBox from './components/customMessageBox'

const App = () => {
  return (
    <GestureHandlerRootView style={{flex: 1}}>
      <NotificationProvider>
        <PaperProvider>
          <Provider store={store}>
              <Navigation />
              <Toast />
              <CustomMessageBox />
          </Provider>
        </PaperProvider>
      </NotificationProvider>
    </GestureHandlerRootView>
  )
}

export default App