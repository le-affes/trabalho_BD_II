import React from 'react';
import { render } from '@testing-library/react';
import { Provider } from 'react-redux';
import store from '../../store';
import Category from '../../pages/categories/[category_slug]';

jest.mock('next/router', () => {
  return {
    useRouter: jest.fn(() => ({
      query: {
        category_slug: 'japonesa',
      },
    })),
  };
});

describe('Category', () => {
  it('should be able to render the category page with a restaurant', () => {
    const { getByText } = render(
      <Provider store={store}>
        <Category
          restaurants={[]}
        />
      </Provider>,
    );

    const title = getByText('Sushi Boulevard');

    const { nodeValue } = title.lastChild;

    expect(nodeValue).toBe('Sushi Boulevard');
  });
});
