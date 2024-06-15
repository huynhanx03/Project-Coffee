import React, { createContext, useState } from 'react';

const NotificationContext = createContext();

const NotificationProvider = ({ children }) => {
  const [isVisible, setIsVisible] = useState(false);
  const [message, setMessage] = useState('');
  const [type, setType ] = useState('');
  const [onClick, setOnClick] = useState(null);

  const showNotification = (message, type, event) => {
    setMessage(message);
    setIsVisible(true);
    setType(type);
    setOnClick(() => event)
  }

  const hideNotification = () => {
    setMessage('');
    setIsVisible(false);
    setType('');
    setOnClick(null);
  }

  return (
    <NotificationContext.Provider value={{ showNotification, hideNotification, message, isVisible, type, onClick }}>
      {children}
    </NotificationContext.Provider>
  );
};

export { NotificationContext, NotificationProvider };
export const useNotification = () => {
    return React.useContext(NotificationContext);
}