import { renderHook } from '@testing-library/react-hooks';
import MockAdapter from 'axios-mock-adapter';
import { useAxios } from '../../hooks/useAxios';
import api from '../../services/api';

const apiMock = new MockAdapter(api);

describe('Axios/SWR hook', () => {
  it('should be able to return a array of food categories', async () => {
    apiMock.onGet('categories').reply(200, []);

    const { result, waitForNextUpdate } = renderHook(() =>
      useAxios('categories'),
    );

    await waitForNextUpdate();

    expect(result.current.data[0].title).toEqual('Lanches');
    expect(result.current.data[1].title).toEqual('Japonesa');
  });

  it('should be able to return a array of foods', async () => {
    apiMock.onGet('foods').reply(200, []);

    const { result, waitForNextUpdate } = renderHook(() => useAxios('foods'));

    await waitForNextUpdate();

    expect(result.current.data[0].title).toEqual('Hamburger');
    expect(result.current.data[1].title).toEqual('Batata frita');
  });
});
