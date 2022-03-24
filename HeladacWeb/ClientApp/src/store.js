import { configureStore, combineReducers } from '@reduxjs/toolkit'
import userReducer from './redux/usersSlice'

const rootReducer = combineReducers({
    user: userReducer,
})

export default configureStore({
  reducer: rootReducer
})