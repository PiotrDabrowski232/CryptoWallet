export interface InputConfig<T = string> {
    label: string,
    type: 'Text' | 'Number',
    value?: T;
}