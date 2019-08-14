import update from '../../helpers/update';
import CaptchaService from './captchaService';


export const KEY_POST_STARTED = "captcha/KEY_POST_STARTED";
export const KEY_POST_SUCCESS = "captcha/KEY_POST_SUCCESS";
export const KEY_POST_FAILED = "captcha/KEY_POST_FAILED";


const initialState = {
    key: {
        data: null,
        error: false,
        loading: false,
        success: false
    },
}

export const captchaReducer = (state = initialState, action) => {
    let newState = state;

    switch (action.type) {

        case KEY_POST_STARTED: {
            ///let {key} = state;
            //const {data,error,loading,success} = state.key;
            // const key = {
            //     data: null,
            //     success: false,
            //     loading: true,
            //     error: false
            // };
            // newState = { ...state, key };
            //newState = { ...state, }
            newState = update.set(state, 'key.loading', true);
            newState = update.set(newState, 'key.success', false);
            newState = update.set(newState, 'key.data', null);
            break;
        }

        default: {
            return newState;
        }
    }

    return newState;
}

export const keyCaptchaActions = {
    started: () => {
        return {
            type: KEY_POST_STARTED
        }
    },

    success: (data) => {
        return {
            type: KEY_POST_SUCCESS,
            payload: data
        }
    },

    failed: (error) => {
        return {
            type: KEY_POST_FAILED
        }
    }
}

export const createNewKey = () => {
    return (dispatch) => {
        dispatch(keyCaptchaActions.started());

        CaptchaService.postNewKey()
            .then((response) => {
                dispatch(keyCaptchaActions.success(response));
            })
            .catch(() => {
                dispatch(keyCaptchaActions.failed());
            });
    }
}